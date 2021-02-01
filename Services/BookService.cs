using BooksLookup.Interfaces;
using BooksLookup.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace BooksLookup.Services
{
    /// <summary>
    /// BookService class is used to provide book services 
    /// for example looking up a book (s) on Open api and updating books.
    /// </summary>
    class BookService : IBookService
    {
        /// <summary>
        /// Instance of the interface that is used to store the injected data 
        /// and call the interface methods.
        /// </summary>
        IBooksDAO _booksDAO;
        /// <summary>
        /// Program Options object to store the injected data from the constructor.
        /// </summary>
        ProgramOptions _programOptions;
        /// <summary>
        /// Instance of the logger object.
        /// </summary>
        private readonly Logger logger = null;
        /// <summary>
        /// Constructor to initialize local variables. 
        /// </summary>
        /// <param name="BooksDAO">Interface Injecting</param>
        /// <param name="programOptions">Object Injecting</param>
        public BookService(IBooksDAO BooksDAO, ProgramOptions programOptions)
        {
            _booksDAO = BooksDAO;
            _programOptions = programOptions;
            logger = new Logger(_programOptions);
        }
        /// <summary>
        /// This method takes in isbn number of a book and searches the Open API for the book.
        /// </summary>
        /// <param name="isbn">isbn of a book</param>
        /// <returns>True if book is found and false if book not found</returns>
        public bool BookLookUp(string isbn)
        {
            bool found = false;
            try
            {
                string clearISBNFormat = Regex.Replace(isbn, @"[^0-9a-zA-Z]+", "", RegexOptions.Compiled);
                if(_booksDAO.GetOnlineBook(clearISBNFormat) != null)
                {
                    found = _booksDAO.GetOnlineBook(clearISBNFormat).found;
                }
            }
            catch (Exception e)
            {
                logger.Log(e.Message);
            }

            return found;
        }
        /// <summary>
        /// Method to look up a list of books on the open library API and sets a found flag to true when
        /// a book is found.
        /// </summary>
        /// <param name="books">List of books to look up</param>
        /// <returns>returns an updated list of all books that are found on the api with found flag true</returns>
        public List<Book> BookLookUp(List<Book> books)
        {
            foreach(Book book in books){
                book.found = BookLookUp(book.isbn);
            }
            return books;
        }

        public Book BookUpdate(Book book)
        {
            try
            {
                string isbn = Regex.Replace(book.isbn, @"[^0-9a-zA-Z]+", "", RegexOptions.Compiled);
                Book onlineBook = _booksDAO.GetOnlineBook(isbn);
                if (onlineBook != null)
                {
                    book.publish_date = onlineBook.publish_date;
                    book.subjects = onlineBook.subjects;
                }
            } catch (Exception e)
            {
                logger.Log(e.Message);
            }
            return book;
        }
        /// <summary>
        /// Updates publish_date and subjects of all books that are found on the 
        /// Open library api.
        /// </summary>
        /// <param name="books">List of books to look up</param>
        /// <returns>returns the updated list of books with subject and publish date set to correct values from
        /// the open libarary api.</returns>
        public List<Book> BookUpdate(List<Book> books)
        {
            try
            {
            foreach(Book book in books)
            {
                book.publish_date= BookUpdate(book).publish_date;
                book.subjects = BookUpdate(book).subjects;
            }
            }catch (Exception e)
            {
                logger.Log(e.Message);
            }

            return books;
        }
    }
}
