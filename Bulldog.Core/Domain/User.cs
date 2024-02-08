using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class User : IdentityUser
    {
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public string? Fullname { get; set; }
        public bool IsConfigured { get; set; }

    }
}
