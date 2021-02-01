using BooksLookup.Interfaces;
using BooksLookup.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using RestSharp;
using Newtonsoft.Json;

namespace BooksLookup.DAO
{
    class BooksDAO : IBooksDAO
    {
        private string inputPath { get; set; }
        private string outputPath { get; set; }
        private string modOutputPath { get; set; }
        private readonly Logger logger = null;
        ProgramOptions _programOptions;
        public BooksDAO(ProgramOptions programOptions)
        {
            _programOptions = programOptions;
            logger = new Logger(_programOptions);
            inputPath = _programOptions.inputLocation;
            outputPath = _programOptions.outputLocation;
        }
        public List<Book> GetBooksFromXML()
        {
            List<Book> books = new List<Book>();
            try
            {
                XElement xmlBooks = XElement.Load(inputPath);
                IEnumerable<XElement> bookList =
                    from element in xmlBooks.Elements()
                    select element;
                foreach (XElement xbook in bookList)
                {
                    Book book = new Book();
                    book.isbn = xbook.Attribute("isbn").Value;
                    if (xbook.Attributes().Count() > 1)
                    {
                        foreach (XAttribute attribute in xbook.Attributes())
                        {
                            switch (attribute.Name.ToString())
                            {
                                case "discounted":
                                    book.discounted = decimal.Parse(attribute.Value);
                                    break;
                                case "found":
                                    book.found = bool.Parse(attribute.Value);
                                    break;
                            }
                        }
                    }
                    book.title = xbook.Element("title").Value;
                    book.publisher = xbook.Element("publisher").Value;
                    book.price = Decimal.Parse(xbook.Element("price").Value);
                    book.pages = int.Parse(xbook.Element("pages").Value);
                    book.chapters = int.Parse(xbook.Element("chapters").Value);
                    book.appendices = int.Parse(xbook.Element("appendices").Value);
                    var authorsCollection = xbook.Element("authors").Descendants().Select(auth => auth.Attributes());
                    book.authors = new List<Author>();
                    foreach (IEnumerable<XAttribute> attributes in authorsCollection)
                    {
                        Author author = new Author();
                        author.firstName = attributes.First().Value;
                        author.lastName = attributes.Last().Value;

                        book.authors.Add(author);
                    }
                    var xReviews = xbook.Element("reviews").Elements();
                    book.reviews = new List<Review>();
                    foreach (XElement element in xReviews)
                    {
                        Review review = new Review();
                        review.evaluation = int.Parse(element.Attribute("eval").Value);
                        review.review = element.Value;
                        book.reviews.Add(review);
                    }
                    books.Add(book);
                }
            }
            catch (Exception e)
            {
                logger.Log(e.Message);
            }

            return books;
        }

        public Book GetOnlineBook(string isbn)
        {
                Book onlineBook = new Book();
            try
            {
                var client = new RestClient("http://openlibrary.org/api");
                var request = new RestRequest($"books?bibkeys={isbn}&jscmd=data&format=json");
                var response = client.Execute(request);
                Dictionary<string, Book> dic = new Dictionary<string, Book>();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string rawResponse = response.Content;
                    dic = JsonConvert.DeserializeObject<Dictionary<string, Book>>(rawResponse);
                    onlineBook = dic.GetValueOrDefault(isbn);
                    if (onlineBook != null)
                        onlineBook.found = true;
                }
                
            }
            catch (Exception e)
            {
                logger.Log(e.Message);
            }
            return onlineBook;
        }

        public void OutputToFile(List<Book> books)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    writer.WriteLine(string.Format(
                                format: "{0,-15}{1,-40}{2,-25}{3,-5:C}{4,8:N0}{5,14}{6,17}{7,50}",
                                "ISBN","Title","Author","Price","Pages","Publish Date","Subject","Path",""));
                    foreach (Book book in books)
                    {
                        if(book.subjects != null)
                        writer.WriteLine(
                            string.Format(
                                format:"{0,-15} {1,-40}{2,-25}{3,-5:C}{4,5:N0}{5,20}{6,38}{7,50}",
                                book.isbn,
                                book.title,
                                string.Join(" ",book.authors.FirstOrDefault().firstName,book.authors.FirstOrDefault().lastName),
                                book.price,
                                book.pages,
                                book.publish_date,
                                book.subjects.FirstOrDefault().name.Trim(),
                                modOutputPath
                                )
                            );
                    }
                }
            } catch (Exception e)
            {
                logger.Log(e.Message);
            }
        }

        public void OutputToFile(List<Book> books, string xmlFileName)
        {
            try {
                XElement root = new XElement("Books");
                foreach (Book book in books)
                {
                    root.Add(
                        new XElement("Book", new XAttribute("isbn", book.isbn), new XAttribute("found", book.found),
                        new XElement("title", book.title),
                        new XElement("publisher", book.publisher),
                        new XElement("price", book.price),
                        new XElement("pages", book.pages),
                        new XElement("chapters", book.chapters),
                        new XElement("appendices", book.appendices),
                        new XElement("authors", book.authors.Select(author =>
                        new XElement("author", new XAttribute("fname", author.firstName), new XAttribute("lname", author.lastName)))),
                        new XElement("reviews", book.reviews.Select(review =>
                         new XElement("review", review.review, new XAttribute("eval", review.evaluation))))));
                    if (book.publish_date != null)
                        root.Add(new XElement("publishDate", book.publish_date));
                    if (book.subjects != null)
                        root.Add(new XElement("subjects", book.subjects.Select(subject => new XElement("subject", subject.name))));
                }
                XDocument doc = new XDocument(root);
                modOutputPath = Path.Combine(Path.GetDirectoryName(outputPath), xmlFileName);
                doc.Save(modOutputPath);

            }
            catch (Exception e)
            {
                logger.Log(e.Message);
            }
        }
    }
}
