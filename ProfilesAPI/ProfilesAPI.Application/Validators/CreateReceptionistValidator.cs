using FluentValidation;
using ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate;

namespace ProfilesAPI.Application.Validators
{
    public class CreateReceptionistValidator : AbstractValidator<CreateReceptionistModel>
    {
        public CreateReceptionistValidator()
        {
            RuleFor(i => i.Info).SetValidator(new HumanInfoValidator());
            RuleFor(i => i.Office.Id).NotEmpty().NotNull();
        }
    }
}
