namespace BooksLookup.Models

{
    /// <summary>
    /// Blue print for book review.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Review score.
        /// </summary>
        public int evaluation { get; set; }
        /// <summary>
        /// Review message body.
        /// </summary>
        public string review { get; set; }
    }
}
