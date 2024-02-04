using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.IServices
{
    public interface IEmailService
    {
        string Email { get; set; }
        bool isNew { get; set; }
        string PhoneNumber { get; set; }
    }
}
