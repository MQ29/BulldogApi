using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Commands.Employees
{
    public class CreateEmployee : ICommand
    {
        public Guid UserId { get; set; }
    }
}
