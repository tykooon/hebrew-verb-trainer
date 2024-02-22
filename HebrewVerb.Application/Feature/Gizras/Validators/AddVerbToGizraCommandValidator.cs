using FluentValidation;
using HebrewVerb.Application.Feature.Gizras.Commands;

namespace HebrewVerb.Application.Feature.Gizras.Validators;

public class AddVerbToGizraCommandValidator : AbstractValidator<AddVerbToGizraCommand>
{
    public AddVerbToGizraCommandValidator()
    {
        RuleFor(d => d.VerbId).GreaterThan(0);

        RuleFor(d => d.GizraName).NotEmpty();
    }
}
