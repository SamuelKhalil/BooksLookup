using BooksLookup.Models;
using System.Collections.Generic;

namespace BooksLookup.Interfaces
{
    public interface IBooksDAO
    {
        public abstract List<Book> GetBooksFromXML();
        public abstract void OutputToFile(List<Book> books);
        public abstract void OutputToFile(List<Book> books, string xmlFileName);
        public Book GetOnlineBook(string isbn);
    }
}
