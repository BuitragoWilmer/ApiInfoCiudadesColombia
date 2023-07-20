using System;

namespace InfoCity.API.Services
{
    public class CloudMailServices : IMailService
    {
        private string mailOrigin = "admin@gmail.com";
        private string mailDestiny = "user@gmail.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"El correo de origen es {mailOrigin} hacia {mailDestiny}"+
                $"con {nameof(CloudMailServices)}");
            Console.WriteLine($"Asunto: {subject}");
            Console.WriteLine($"mensaje: {message}");

        }
    }
}
