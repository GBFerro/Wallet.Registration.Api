using FluentValidation;

namespace Wallet.Registration.Domain.Command.v1.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(signIn => signIn.Password)
                .NotEmpty().WithMessage("PasswordRequired")
                .MinimumLength(8).WithMessage("PasswordMinLength")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&#ç´`^~:,.;]{8,}$").WithMessage("StrongPasswordRequired");

            RuleFor(signIn => signIn.Username)
                .NotEmpty().WithMessage("UsernameRequired");

            RuleFor(signIn => signIn.User).ChildRules(user =>
            {
                user.RuleFor(userProperties => userProperties.Name)
                    .NotEmpty().WithMessage("NameRequired");

                user.RuleFor(userProperties => userProperties.BirthDate)
                    .NotEmpty().WithMessage("BirthDateRequired")
                    .Matches("^(?:0[1-9]|[12]\\d|3[01])([\\/.-])(?:0[1-9]|1[012])\\1(?:19|20)\\d\\d$").WithMessage("DateFormatRequired");

                user.RuleFor(userProperties => userProperties.Email)
                    .NotEmpty().WithMessage("EmailRequired");

                user.RuleFor(userProperties => userProperties.GovId)
                    .NotEmpty().WithMessage("GovIdRequired");
                //.InclusiveBetween("11", "11").WithMessage("GovIdLenght");
            });
        }
    }
}
