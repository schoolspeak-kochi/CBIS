using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Utils
{
    public static class Logger
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void LogException(this Exception ex)
        {
            _logger.Error(ex, "Error:");
        }

        /// <summary>
        /// Logs the specified message as an Error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void LogError(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogMessage(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Logs a warning.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogWarning(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static string GetErrorMessage(Exception ex)
        {
#if DEBUG
            return ex.Message;
#else
            return string.Format("Sorry! An unexpected error has occurred at {0}. Please contact your administrator.", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));;
#endif

        }

        /// <summary>
        /// Writes a message if in DEBUG mode.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteDebugMessage(string message)
        {
#if DEBUG
            _logger.Debug(message);
#endif
        }
    }
}
