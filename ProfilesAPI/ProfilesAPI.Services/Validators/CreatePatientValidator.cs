using FluentValidation;
using ProfilesAPI.Services.Abstraction.AggregatesModels.PatientAggregate;

namespace ProfilesAPI.Services.Validators
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientModel>
    {
        private const string _phoneRegex = "^(\\+)?((\\d{2,3}) ?\\d|\\d)(([ -]?\\d)|( ?(\\d{2,3}) ?)){5,12}\\d$";
        private const string _emailRegex = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
        public CreatePatientValidator()
        {
            RuleFor(i => i.Info.FirstName).NotEmpty().NotNull();
            RuleFor(i => i.Info.LastName).NotEmpty().NotNull();
            RuleFor(i => i.Info.MiddleName).NotEmpty().NotNull();
            RuleFor(i => i.Info.BirthDay).NotEmpty().NotNull();
            RuleFor(i => i.Info.Email).NotEmpty()
                                 .NotNull()
                                 .Matches(_emailRegex);

            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches(_phoneRegex);
        }
    }
}
