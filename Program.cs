using System;
using System.Collections.Generic;
using System.IO;
using BooksLookup.DAO;
using BooksLookup.Interfaces;
using BooksLookup.Models;
using BooksLookup.Services;
using MatthiWare.CommandLine;
using static System.Console;
namespace BooksLookup
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new CommandLineParser<ProgramOptions>();
            var result = parser.Parse(args);
            if (result.HasErrors)
            {
                Console.Error.WriteLine("parsing has errors...!");
                return;
            }
            var programOptions = result.Result;

            WriteLine("program executed with: ");
            Console.WriteLine($"input location: {programOptions.inputLocation}");
            Console.WriteLine($"output location: {programOptions.outputLocation}");
            Console.WriteLine($"Log Errors: {programOptions.loggerFlag}");
            BooksDAO booksDAO = new BooksDAO(programOptions);
            BookService bookService = new BookService(booksDAO, programOptions);
            List<Book> books = new List<Book>();
            books = booksDAO.GetBooksFromXML();
            bookService.BookLookUp(books);
            bookService.BookUpdate(books);
            booksDAO.OutputToFile(books, "mod.xml");
            booksDAO.OutputToFile(books);
        }
    }
}
