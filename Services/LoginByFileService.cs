using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SecureApp.Services
{
    public class LoginByFileService
    {
        private readonly IConfiguration configuration;
      

        public LoginByFileService(IConfiguration configuration)
        {
            this.configuration = configuration;
         
        }

        public (string, string) GetLoginInfo()
        {
            string[] loginInfo = File.ReadAllLines(configuration["LoginFile"]);

            if (loginInfo.Length != 2)
            {
                throw new Exception("LoginFileProvider expects 2 lines in the login file. First one is the user and second one is the pass!");
            }
            else if(String.IsNullOrWhiteSpace(loginInfo[0]) || String.IsNullOrWhiteSpace(loginInfo[1]))
            {
                throw new Exception("Username or Password can not be whitespace");
            }

            return (loginInfo[0], loginInfo[1]);
        }

    }
}
