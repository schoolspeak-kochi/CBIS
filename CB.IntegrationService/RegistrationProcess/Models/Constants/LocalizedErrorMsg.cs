using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationProcess.Models.Constants
{
    public class LocalizedErrorMsg
    {
        public static string ProductNameRequiredMsg
        {
            get {
                return "Please enter product name.";
            }
        }
        
        public static string ProductEndPointRequiredMsg
        {
            get {
                return "Please enter product endpoint URL.";
               }
        }

        public static string ProductSecretRequiredMsg
        {
            get
            {
                return "Please enter a secret key.";
            }
        }
        public static string EmailNotValidMsg
        {
            get
            {
                return "Please provide a valid email.";                 
            }
        }
    }
}