using FluentValidation;
using ProfilesAPI.Services.Abstraction.AggregatesModels.DoctorAggregate;

namespace ProfilesAPI.Services.Validators
{
    public class CreateDoctorValidator : AbstractValidator<CreateDoctorModel>
    {
        public CreateDoctorValidator()
        {
            RuleFor(i => i.Info).SetValidator(new HumanInfoValidator());
            RuleFor(i => i.Specialization).NotEmpty().NotNull();
            RuleFor(i => i.OfficeId).NotEmpty().NotNull();
            RuleFor(i => i.CareerStartYear).NotEmpty().GreaterThan(1900);
        }
    }
}
