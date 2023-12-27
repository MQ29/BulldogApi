using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Commands.Users
{
    public class CreateUser : ICommand
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
