using CB.IntegrationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Utils
{
    /// <summary>
    /// Notify error by send mail to with the detailed exception list.
    /// </summary>
    public static class ErrorNotifyHelper
    {
        private static string Recipient = "akhil3203@gmail.com";

        /// <summary>
        /// Sender internal server error message notification to the recipients. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public static void InternalError(string message = "", Exception error = null)
        {
            EmailHelper emailHelper = new EmailHelper();
            EmailPayload payload = new EmailPayload();
            payload.Recipients = new List<string>() { Recipient };
            payload.Subject = "Internal server error";
            payload.Body = message;
            if (error != null)
                payload.Body += "<br/>" + error.Message;

            try
            {
                emailHelper.SendMail(payload);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Sender internal server error message notification to the recipients. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public static void ClientError(string subject = "", string message = "", Exception error = null)
        {
            EmailHelper emailHelper = new EmailHelper();
            EmailPayload payload = new EmailPayload();
            payload.Recipients = new List<string>() { Recipient };
            payload.Subject = subject;
            payload.Body = message;
            if (error != null)
                payload.Body += "<br/>" + error.Message;

            try
            {
                emailHelper.SendMail(payload);
            }
            catch (Exception)
            {

            }
        }

    }
}
