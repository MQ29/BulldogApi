namespace BulldogApiFrontend.Identity.Models
{
    public class UserInfo
    {
        public string Email { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// A value indicating whether the email has been confirmed yet.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// The list of claims for the user.
        /// </summary>
        public Dictionary<string, string> Claims { get; set; }
         = [];
    }
}
