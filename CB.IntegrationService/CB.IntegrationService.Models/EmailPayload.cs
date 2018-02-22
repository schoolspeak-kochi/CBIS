using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Models
{
    public class EmailPayload
    {
        /// <summary>
        /// Gets or sets the email recipient.
        /// If not specified, default settings will be used.
        /// </summary>
        public string Sender { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the email recipients list.
        /// </summary>
        public List<string> Recipients { get; set; }

        /// <summary>
        /// Gets or sets the email Subject
        /// </summary>
        public string Subject { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the email body
        /// </summary>
        public string Body { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the email text
        /// The email body for recipients with non-HTML email clients.
        /// </summary>
        public string Text { get; set; } = String.Empty;
    }
}
