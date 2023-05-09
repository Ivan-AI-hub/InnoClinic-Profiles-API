using FluentValidation;
using ProfilesAPI.Application.Commands.Patients.Create;

namespace ProfilesAPI.Application.Validators
{
    public class CreatePatientValidator : AbstractValidator<CreatePatient>
    {
        private const string _phoneRegex = "^(\\+)?((\\d{2,3}) ?\\d|\\d)(([ -]?\\d)|( ?(\\d{2,3}) ?)){5,12}\\d$";
        public CreatePatientValidator()
        {
            RuleFor(x => x.HumanInfo).SetValidator(new HumanInfoValidator());
            RuleFor(x => x.phoneNumber).NotEmpty().NotNull().Matches(_phoneRegex);
        }
    }
}
