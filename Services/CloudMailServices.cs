using Microsoft.Extensions.Configuration;
using System;

namespace InfoCity.API.Services
{
    public class CloudMailServices : IMailService
    {
        private readonly string mailOrigin = string.Empty;
        private readonly string mailDestiny = string.Empty;

        public CloudMailServices(IConfiguration configuration)
        {
            this.mailOrigin = configuration["mailSettings:mailFromAddress"];
            this.mailDestiny = configuration["mailSettings:mailToAddress"];
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"El correo de origen es {mailOrigin} hacia {mailDestiny}"+
                $"con {nameof(CloudMailServices)}");
            Console.WriteLine($"Asunto: {subject}");
            Console.WriteLine($"mensaje: {message}");

        }
    }
}
