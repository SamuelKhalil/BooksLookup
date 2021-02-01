using BooksLookup.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksLookup.Interfaces
{
    public interface IBookService
    {
        public abstract List<Book> BookLookUp(List<Book> books);

        public abstract Book BookUpdate(Book book);

        public abstract bool BookLookUp(string isbn);

        public abstract List<Book> BookUpdate(List<Book> books);
    }
}
