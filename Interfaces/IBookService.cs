using BooksLookup.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksLookup.Interfaces
{
    /// <summary>
    /// Interface to the book service that also gives a blue print of the methods 
    /// that are used in the BookService class.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Abstract method that is used to lookup a list of books and has it's
        /// implementation in the BookService Class.
        /// </summary>
        /// <param name="books">List of books to be looked up</param>
        /// <returns>returns an updated list of found books</returns>
        public abstract List<Book> BookLookUp(List<Book> books);
        /// <summary>
        /// Abstract method that is used to update a book with published date and
        /// subjects from the Open library API.
        /// </summary>
        /// <param name="book">book to be updated</param>
        /// <returns>The updated book</returns>
        public abstract Book BookUpdate(Book book);
        /// <summary>
        /// An abstract method that is used to look up a book on the Open Library API
        /// and returns a bool flag if book is found or not.
        /// </summary>
        /// <param name="isbn">isbn of a book</param>
        /// <returns>True if book is found and false if book not found</returns>
        public abstract bool BookLookUp(string isbn);
        /// <summary>
        /// An abstract method that is used to update a list of books with publish
        /// date and list of subject from the open library api.
        /// </summary>
        /// <param name="books">List of books to look up</param>
        /// <returns>List of books that are found in the open library</returns>
        public abstract List<Book> BookUpdate(List<Book> books);
    }
}
