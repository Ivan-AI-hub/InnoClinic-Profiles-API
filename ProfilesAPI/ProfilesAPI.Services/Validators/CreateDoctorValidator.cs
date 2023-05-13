using FluentValidation;
using ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate;

namespace ProfilesAPI.Services.Validators
{
    public class CreateDoctorValidator : AbstractValidator<CreateDoctorModel>
    {
        private const string _emailRegex = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
        public CreateDoctorValidator()
        {
            RuleFor(i => i.FirstName).NotEmpty().NotNull();
            RuleFor(i => i.LastName).NotEmpty().NotNull();
            RuleFor(i => i.MiddleName).NotEmpty().NotNull();
            RuleFor(i => i.BirthDay).NotEmpty().NotNull();
            RuleFor(i => i.Specialization).NotEmpty().NotNull();
            RuleFor(i => i.OfficeId).NotEmpty().NotNull();
            RuleFor(i => i.CareerStartYear).NotEmpty().GreaterThan(1900);
            RuleFor(i => i.Email).NotEmpty()
                                 .NotNull()
                                 .Matches(_emailRegex);

        }
    }
}
