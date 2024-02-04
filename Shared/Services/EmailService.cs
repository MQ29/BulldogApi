using Shared.IServices;

namespace Shared.Services
{
    public class EmailService : IEmailService
    {
        public string Email { get; set; }
        public bool isNew { get; set; }
        public string PhoneNumber { get; set; }
    }
}
