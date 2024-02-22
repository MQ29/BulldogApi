using Bulldog.Core.Domain;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BulldogApiFrontend.Models
{
    public class RegistrationValidationVm : AbstractValidator<RegisterModel>
    {
        private readonly HttpClient _httpClient;
        public RegistrationValidationVm(HttpClient httpClient)
        {
            RuleFor(x => x.PhoneNumber).NotEmpty()
            .Matches(@"^\d{9}$").WithMessage("Phone number must contain exactly 9 digits")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
            RuleFor(x => x.Fullname).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress()
                .When(_ => !string.IsNullOrEmpty(_.Email) && Regex.IsMatch(_.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase), ApplyConditionTo.CurrentValidator)
                .WithMessage("Email Is Already Exist");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(6).WithMessage("Your password length must be at least 6.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\@\!\?\*\.]+").WithMessage("Your password must contain at least one (@!? *.).");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result =
            await ValidateAsync(ValidationContext<RegisterModel>.CreateWithOptions((RegisterModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

        
    }
}
