using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InfoCity.API.Controllers
{
    [Route("api/autenticacion")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("autenticar")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody requestBody)
        {
            //Paso 1: Validar las credenciales
            var user = ValidateUserCredentials(requestBody.Username, requestBody.Password);
            if (user == null) 
            {
                return Unauthorized();    
            }
            //Paso 2: Crear token
            ///Llave en binario
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:SecretForKey"]));
            ///Firma
            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            ///Encabezado
            var claimsToken = new List<Claim>();
            claimsToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsToken.Add(new Claim("given_name", user.FirstName));
            claimsToken.Add(new Claim("family_name", user.LastName));
            claimsToken.Add(new Claim("city", user.CityId.ToString()));
            ///Generar token
            var jwtSecurityToken = new JwtSecurityToken(
                    configuration["Authentication:Issuer"],
                    configuration["Authentication:Audience"],
                    claimsToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signingCredential
                );
            var tokenReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenReturn);
        }

        private CityInfoUser ValidateUserCredentials(string username, string password)
        {
            //Revisar que existen dichas credenciales en BD, en este momento se da por hecho que existe el usuario

            return new CityInfoUser(1, 1, username ?? "", "Jineth", "Leon");
        }

        public class AuthenticationRequestBody
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        public class CityInfoUser
        {
            public int UserId { get; set; }
            public int CityId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public CityInfoUser(int userId, int city, string userName, string firstName, string lastName)
            {
                UserId = userId;
                CityId = city;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
            }
        }
    }
}
