using FluentValidation;

namespace HebrewVerb.Application.Feature.VerbModels.Commands;

public class AddVerbToModelCommandValidator : AbstractValidator<AddVerbToModelCommand>
{
    public AddVerbToModelCommandValidator()
    {
        RuleFor(d => d.VerbId).GreaterThan(0);

        RuleFor(d => d.VerbModelName).NotEmpty();
    }
}
