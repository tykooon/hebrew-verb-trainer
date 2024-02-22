using FluentValidation;
using HebrewVerb.Application.Feature.Gizras.Commands;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Feature.Gizras.Validators;

public class AddNewGizraCommandValidator : AbstractValidator<AddNewGizraCommand>
{
    public AddNewGizraCommandValidator()
    {
        RuleFor(d => d.Name).NotEmpty();
        RuleFor(d => d.Binyans)
            .ForEach(b => b.Must(b => Binyan.TryFromName(b, out _)));
    }
}
