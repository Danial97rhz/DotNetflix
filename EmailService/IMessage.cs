using System.Net.Mail;

namespace EmailService
{
    public interface IMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Attachment Attachment { get; set; }
    }
}
