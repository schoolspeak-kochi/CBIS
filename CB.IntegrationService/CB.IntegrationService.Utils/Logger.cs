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
        /// Log the Exceptions 
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void LogException(this Exception ex)
        {
            _logger.Error(ex, "Error:");
        }

        /// <summary>
        /// Log the error
        /// </summary>
        /// <param name="message">The error Message</param>
        public static void LogError(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Log the Information
        /// </summary>
        /// <param name="message">The information Message</param>
        public static void LogInfo(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Logs warning.
        /// </summary>
        /// <param name="message">The warning message.</param>
        public static void LogWarning(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Logs the Trace.
        /// </summary>
        /// <param name="message">The trace</param>
        public static void LogTrace(string message)
        {
            _logger.Trace(message);
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

        /// <summary>
        /// Logs the failure and send the error notification.
        /// </summary>
        /// <param name="message">The trace</param>
        public static void LogErrorAndNotify(this Exception ex)
        {
            _logger.Error(ex,"Error: ");
            // Write code to implement the error  notification mechanism
        }
    }
}
