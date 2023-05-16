using FluentValidation;
using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;

namespace ProfilesAPI.Services.Validators
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
