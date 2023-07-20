using System;

namespace InfoCity.API.Services
{
    public class LocalMailService : IMailService
    {
        private string mailOrigin = "admin@gmail.com";
        private string mailDestiny = "user@gmail.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"El correo de origen es {mailOrigin} hacia {mailDestiny}");
            Console.WriteLine($"Asunto: {subject}");
            Console.WriteLine($"mensaje: {message}");

        }
    }
}
