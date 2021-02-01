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
    class BookService : IBookService
    {
        IBooksDAO _booksDAO;
        ProgramOptions _programOptions;
        private readonly Logger logger = null;
        public BookService(IBooksDAO BooksDAO, ProgramOptions programOptions)
        {
            _booksDAO = BooksDAO;
            _programOptions = programOptions;
            logger = new Logger(_programOptions);
        }

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
