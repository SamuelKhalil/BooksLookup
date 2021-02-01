
using MatthiWare.CommandLine.Core.Attributes;
namespace BooksLookup.Models
{
    /// <summary>
    /// ProgramOptions that are entered by the user include input file 
    /// path, output file path, and a flag of where to log  errors or not.
    /// </summary>
    public class ProgramOptions
    {
        /// <summary>
        /// A required parameter that is entered by the user and represents 
        /// the input file path.
        /// </summary>
        [Required, Name("Input", "inputLocation"), Description("Bath to the input file for example Books.xml")]
        public string inputLocation { get; set; }
        [Required, Name("Output", "outputLocation"), Description("Bath to the input file for example Output.txt")]
        /// <summary>
        /// A required parameter that is entered by the user and represents 
        /// the output file path.
        /// </summary>
        public string outputLocation { get; set; }
        [Name("E", "loggerFlag"), Description("Add -E flag if you want to Log errors, erros will be logged to the same location as you input file"), DefaultValue(false)]
        /// <summary>
        /// Not a required parameter that is entered by the user and represents 
        /// a log flag to either print to a file or not print to a file.
        /// </summary>
        public bool loggerFlag { get; set; }
    }
}
