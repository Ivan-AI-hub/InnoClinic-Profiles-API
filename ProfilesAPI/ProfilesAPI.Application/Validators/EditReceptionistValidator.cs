using FluentValidation;
using ProfilesAPI.Application.Abstraction.AggregatesModels.ReceptionistAggregate;

namespace ProfilesAPI.Application.Validators
{
    public class EditReceptionistValidator : AbstractValidator<EditReceptionistModel>
    {
        public EditReceptionistValidator()
        {
            RuleFor(i => i.FirstName).NotEmpty().NotNull();
            RuleFor(i => i.LastName).NotEmpty().NotNull();
            RuleFor(i => i.MiddleName).NotEmpty().NotNull();
            RuleFor(i => i.BirthDay).NotEmpty().NotNull();
            RuleFor(i => i.OfficeId).NotEmpty().NotNull();
        }
    }
}
