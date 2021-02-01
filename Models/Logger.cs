using BooksLookup.Interfaces;
using System;
using System.IO;

namespace BooksLookup.Models
{
    class Logger: ILogger
    {
        private string fileName { get; set; }
        private string filePath { get; set; }
        ProgramOptions _programOptions;
        public Logger(ProgramOptions programOptions)
        {
            _programOptions = programOptions;
            this.fileName = "error.log";
            this.filePath = Path.Combine(Path.GetDirectoryName(_programOptions.outputLocation), fileName);
        }

        public void Log(string message)
        {
            if(_programOptions.loggerFlag == true)
            {
                using(StreamWriter writer = File.AppendText(this.filePath))
                {
                    writer.WriteLine("{0}  {1}  {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToShortTimeString(), message );
                }
            }
        }
    }
}  
