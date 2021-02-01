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
    /// <summary>
    /// This is the start up class that is to be run first when the application 
    /// is fired up.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Start up method that is run first when the application is started 
        /// and it is used to get initial inputs from user as well as 
        /// invoking methods that are the backbone of this application.
        /// </summary>
        /// <param name="args">The input paramters entered by the user when 
        /// application is started</param>
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
