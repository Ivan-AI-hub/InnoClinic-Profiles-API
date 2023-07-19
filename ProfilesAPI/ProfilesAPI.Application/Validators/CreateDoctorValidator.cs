using FluentValidation;
using ProfilesAPI.Application.Abstraction.AggregatesModels.DoctorAggregate;

namespace ProfilesAPI.Application.Validators
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
