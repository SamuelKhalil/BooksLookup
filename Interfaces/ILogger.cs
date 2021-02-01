using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLookup.Interfaces
{
    /// <summary>
    /// Interface of logger class.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// abstract method that is implemented in the logger class 
        /// and used to log a message to a file.
        /// </summary>
        /// <param name="message"></param>
        public abstract void Log(string message);
    }
}
