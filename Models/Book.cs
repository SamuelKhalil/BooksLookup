using System;
using System.Collections.Generic;
namespace BooksLookup.Models

{
    /// <summary>
    /// Blue print for book objects.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Book isbn.
        /// </summary>
        public string isbn { get; set; }
        /// <summary>
        /// Book found flag
        /// </summary>
        public bool found { get; set; }
        /// <summary>
        /// book title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// book publisher
        /// </summary>
        public string publisher { get; set; }
        /// <summary>
        /// book price
        /// </summary>
        public Decimal price { get; set; }
        /// <summary>
        /// book number pages
        /// </summary>
        public int pages { get; set; }
        /// <summary>
        /// book number of chapters
        /// </summary>
        public int chapters { get; set; }
        /// <summary>
        /// number of appendices
        /// </summary>
        public int appendices { get; set; }
        /// <summary>
        /// discount on books
        /// </summary>
        public decimal discounted { get; set; }
        /// <summary>
        /// list of book authors
        /// </summary>
        public List<Author> authors { get; set; }
        /// <summary>
        /// list of book reviews
        /// </summary>
        public List<Review> reviews { get; set; }
        /// <summary>
        /// Date book was published
        /// </summary>
        public string publish_date { get; set; }
        /// <summary>
        /// List of book subjects.
        /// </summary>
        public List<Subject> subjects { get; set; }
    }
}
