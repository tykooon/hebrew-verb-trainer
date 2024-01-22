using FluentValidation;

namespace HebrewVerb.Application.Feature.Gizras.Commands;

public class AddVerbToGizraCommandValidator : AbstractValidator<AddVerbToGizraCommand>
{
    public AddVerbToGizraCommandValidator()
    {
        RuleFor(d => d.VerbId).GreaterThan(0);

        RuleFor(d => d.GizraName).NotEmpty();
    }
}
