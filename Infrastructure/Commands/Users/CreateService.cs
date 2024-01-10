using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Commands.Users
{
    public class CreateService : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
