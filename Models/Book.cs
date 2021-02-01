using System;
using System.Collections.Generic;
namespace BooksLookup.Models

{
    public class Book
    {
        public string isbn { get; set; }
        public bool found { get; set; }
        public string title { get; set; }
        public string publisher { get; set; }
        public Decimal price { get; set; }
        public int pages { get; set; }
        public int chapters { get; set; }
        public int appendices { get; set; }
        public decimal discounted { get; set; }
        public List<Author> authors { get; set; }
        public List<Review> reviews { get; set; }
        public string publish_date { get; set; }
        public List<Subject> subjects { get; set; }
    }
}
