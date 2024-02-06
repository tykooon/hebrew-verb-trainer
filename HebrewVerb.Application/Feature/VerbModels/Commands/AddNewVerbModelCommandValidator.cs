using FluentValidation;

namespace HebrewVerb.Application.Feature.VerbModels.Commands;

public class AddNewVerbModelCommandValidator : AbstractValidator<AddNewVerbModelCommand>
{
    public AddNewVerbModelCommandValidator()
    {
        RuleFor(d => d.Name).NotEmpty();
    }
}
