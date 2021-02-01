using BooksLookup.Models;
using System.Collections.Generic;

namespace BooksLookup.Interfaces
{
    /// <summary>
    /// Interface of books data access object that is used to access the methods 
    /// of the BooksDAO and creates a blue print for the BooksDAO.
    /// </summary>
    public interface IBooksDAO
    {
        /// <summary>
        /// Abstract method that has an implementation in the BooksDAO class.
        /// The method is suppose to Load the books data from xml file.
        /// </summary>
        /// <returns>returns a list of books that has been loaded to memory
        /// from the xml file</returns>
        public abstract List<Book> GetBooksFromXML();
        /// <summary>
        /// Abstract method that has it's implementation in the BooksDAO class.
        /// The method is suppose to output the books list to a text file in a 
        /// tab delimited format.
        /// </summary>
        /// <param name="books">List of books to be used to print out to the 
        /// text file</param>
        public abstract void OutputToFile(List<Book> books);
        /// <summary>
        /// Abstract method that has it's implmentation in the BooksDAO class.
        /// This method is suppose to output a list of books as xml file.
        /// </summary>
        /// <param name="books">List of books to be printed out to the file</param>
        /// <param name="xmlFileName">File name that will contain the printed out data</param>
        public abstract void OutputToFile(List<Book> books, string xmlFileName);
        /// <summary>
        /// This abstract method has it's implementation in the BooksDAO class.
        /// This method is suppose to retrieve the books from open library API
        /// </summary>
        /// <param name="isbn">ISBN number</param>
        /// <returns>Metadata from the open library api</returns>
        public Book GetOnlineBook(string isbn);
    }
}
