using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class User
    {
        private static readonly Regex UsernameRegex = new Regex("^ [a - z0 - 9_ -]{3, 15}$");
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        //public string Fullname { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
            
        }
        public User(string email, string username, string password, string salt)
        {
            Id = Guid.NewGuid();
            Email = email;
            Username = username;
            Password = password;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email cant be empty.");
            if (Email == email)
                return;
            Email = email;
        }
        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password cant be empty.");
            if (Password == password)
                return;
            Password = password;
        }
        public void SetUsername(string username)
        {
            if (!UsernameRegex.IsMatch(username))
                throw new Exception("Username is invalid.");
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Username cant be empty.");
            if (Username == username)
                return;
            Username = username;
        }
    }
}
