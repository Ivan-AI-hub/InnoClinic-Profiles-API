using FluentValidation;
using ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate;

namespace ProfilesAPI.Services.Validators
{
    public class EditDoctorValidator : AbstractValidator<EditDoctorModel>
    {
        public EditDoctorValidator()
        {
            RuleFor(i => i.FirstName).NotEmpty().NotNull();
            RuleFor(i => i.LastName).NotEmpty().NotNull();
            RuleFor(i => i.MiddleName).NotEmpty().NotNull();
            RuleFor(i => i.BirthDay).NotEmpty().NotNull();
            RuleFor(i => i.Specialization).NotEmpty().NotNull();
            RuleFor(i => i.OfficeId).NotEmpty().NotNull();
            RuleFor(i => i.CareerStartYear).NotEmpty().GreaterThan(1900);
        }
    }
}
