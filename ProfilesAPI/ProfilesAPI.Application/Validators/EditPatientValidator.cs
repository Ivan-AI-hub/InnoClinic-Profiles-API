using FluentValidation;
using ProfilesAPI.Application.Abstraction.AggregatesModels.PatientAggregate;

namespace ProfilesAPI.Application.Validators
{
    public class EditPatientValidator : AbstractValidator<EditPatientModel>
    {
        public EditPatientValidator()
        {
            RuleFor(i => i.FirstName).NotEmpty().NotNull();
            RuleFor(i => i.LastName).NotEmpty().NotNull();
            RuleFor(i => i.MiddleName).NotEmpty().NotNull();
            RuleFor(i => i.BirthDay).NotEmpty().NotNull();
            RuleFor(i => i.PhoneNumber).NotEmpty().NotNull();
        }
    }
}
