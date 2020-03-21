using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace EmailService
{
    public class Message : IMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Attachment Attachment { get; set; }

        /// <summary>
        /// Method to sent a email message
        /// </summary>
        /// <param name="to">Email address of the reciver</param>
        /// <param name="subject">Subject of the message</param>
        /// <param name="content">Content of the message</param>
        public Message(string to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Content = content;
        }

        /// <summary>
        /// Method to sent a email message
        /// </summary>
        /// <param name="to">Email address of the reciver</param>
        /// <param name="subject">Subject of the message</param>
        /// <param name="attachment">Path to attachmet</param>
        public Message(string to, string subject, string content, Attachment attachment)
        {
            To = to;
            Subject = subject;
            Content = content;
            Attachment = attachment;
        }
    }
}
