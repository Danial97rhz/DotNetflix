using Microsoft.Extensions.Logging;

namespace DotNetflix.API.Services
{
    public class INullMailService : IMailService
    {
        private readonly ILogger<INullMailService> _logger;

        public INullMailService(ILogger<INullMailService> logger)
        {
            _logger = logger;

        }

        public void SendMessage(string to, string subject, string body)
        {
            _logger.LogInformation($"To: {to} Subject: {subject} Body:{body}");


        }
    }
}