using BooksLookup.Interfaces;
using System;
using System.IO;

namespace BooksLookup.Models
{
    /// <summary>
    /// Logger class that is used to log error to a file called error.log
    /// located in the same path as the output paramter provided by the user on app
    /// start up.
    /// </summary>
    class Logger: ILogger
    {
        /// <summary>
        /// local variable that store the name of the log file.
        /// </summary>
        private string fileName { get; set; }
        /// <summary>
        /// local variable that store the path of the log file.
        /// </summary>
        private string filePath { get; set; }
        /// <summary>
        /// This is an istance of the program options object.
        /// </summary>
        ProgramOptions _programOptions;
        public Logger(ProgramOptions programOptions)
        {
            _programOptions = programOptions;
            this.fileName = "error.log";
            this.filePath = Path.Combine(Path.GetDirectoryName(_programOptions.outputLocation), fileName);
        }
        /// <summary>
        /// This method is used to log a message to a file.
        /// </summary>
        /// <param name="message">message to be logged</param>
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
