using FluentValidation;
using HebrewVerb.Application.Feature.Verbs.Commands;

namespace HebrewVerb.Application.Feature.Verbs.Validators;

public class AddNewVerbCommandValidator : AbstractValidator<AddNewVerbCommand>
{
    public AddNewVerbCommandValidator()
    {
        RuleFor(c => c.VerbDto.Binyan).NotEmpty();
        RuleFor(c => c.VerbDto.Infinitive.Hebrew).NotEmpty();
    }

}
