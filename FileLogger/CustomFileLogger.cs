///<summary>
/// 
/// Authors:   Hannah Larsen and Todd Oldham
/// Date:      3/30/2023
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500, Hannah Larsen, Todd Oldham - This work may not 
///            be copied for use in Academic Coursework.
///
/// I, Hannah Larsen and Todd Oldham, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All 
/// references used in the completion of the assignments are cited 
/// in my README file.
///
/// File Contents
/// 
/// This file contains the classes needed to implement logging in our assignment
/// Here we define how the log messages are made and what file they are saved to.
/// 
/// </summary>

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLogger
{
    /// <summary>
    /// Actual logger class
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly string categoryName;
        private string _FileName;

        /// <summary>
        /// pass in different category names to write to different files
        /// </summary>
        /// <param name="catName"> the category name </param>
        public FileLogger(string catName)
        {
            categoryName = catName;
            _FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
               + Path.DirectorySeparatorChar
               + $"CS3500-{categoryName}.log";
        }

        /// <summary>
        /// Microsofts definition of a scope is, "A scope can group a set of logical operations. 
        /// This grouping can be used to attach the same data to each log that's created as part of a set."
        /// </summary>
        /// <param name="state"> the state </param>
        /// <returns> and IDisposable </returns>
        /// <exception cref="NotImplementedException"></exception>
        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not used in this assignment
        /// </summary>
        /// <param name="logLevel"> the log level </param>
        /// <returns> true or false </returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method logs all of the messages that we print during the operation of the GUIs and backing code
        /// </summary>
        /// <param name="logLevel"> the log level of the message </param>
        /// <param name="eventId"> the event id </param>
        /// <param name="state"> the state </param>
        /// <param name="exception"> the exception </param>
        /// <param name="formatter"> the formatter </param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            File.AppendAllText(_FileName, DateTime.Now.ToString());
            // the thread id, debug level, and message will all be stored in the debug message
            File.AppendAllText(_FileName, formatter(state, exception));
            File.AppendAllText(_FileName, Environment.NewLine);
        }
    }



    /// <summary>
    /// Wrapper class for the logger
    /// </summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// Return a new file logger with the given category name
        /// </summary>
        /// <param name="categoryName"> the category name </param>
        /// <returns> and ILogger </returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName);
        }

        /// <summary>
        /// Not used in this assignment
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}


