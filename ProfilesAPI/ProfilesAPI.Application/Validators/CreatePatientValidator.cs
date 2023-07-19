using FluentValidation;
using ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate;

namespace ProfilesAPI.Application.Validators
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientModel>
    {
        private const string _phoneRegex = "^(\\+)?((\\d{2,3}) ?\\d|\\d)(([ -]?\\d)|( ?(\\d{2,3}) ?)){5,12}\\d$";
        public CreatePatientValidator()
        {
            RuleFor(i => i.Info).SetValidator(new HumanInfoValidator());
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches(_phoneRegex);
        }
    }
}
