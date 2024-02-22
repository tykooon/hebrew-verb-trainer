using FluentValidation;
using HebrewVerb.Application.Feature.VerbModels.Commands;

namespace HebrewVerb.Application.Feature.VerbModels.Validators;

public class AddVerbToModelCommandValidator : AbstractValidator<AddVerbToModelCommand>
{
    public AddVerbToModelCommandValidator()
    {
        RuleFor(d => d.VerbId).GreaterThan(0);

        RuleFor(d => d.VerbModelName).NotEmpty();
    }
}
