
using MatthiWare.CommandLine.Core.Attributes;
namespace BooksLookup.Models
{
    public class ProgramOptions
    {
        [Required, Name("Input", "inputLocation"), Description("Bath to the input file for example Books.xml")]
        public string inputLocation { get; set; }
        [Required, Name("Output", "outputLocation"), Description("Bath to the input file for example Output.txt")]
        public string outputLocation { get; set; }
        [Name("E", "loggerFlag"), Description("Add -E flag if you want to Log errors, erros will be logged to the same location as you input file"), DefaultValue(false)]
        public bool loggerFlag { get; set; }
    }
}
