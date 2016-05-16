using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BootstrapIntroduction.Models
{
    public class User:IIdentity
    {
        public User(string username, string password,
            string[] roles, List<string> validIpAddresses)
        {
            Name = username;
            Password = password;
            Roles = roles;
            ValidIpAddresses = validIpAddresses;
        }

        public string Name { get; private set; }

        public string Password { get; private set; }

        public string[] Roles { get; private set; }

        public List<string> ValidIpAddresses { get; private set; }
        
        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}