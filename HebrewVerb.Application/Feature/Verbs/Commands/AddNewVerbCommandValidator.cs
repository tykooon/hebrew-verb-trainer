using FluentValidation;

namespace HebrewVerb.Application.Feature.Verbs.Commands;

public class AddNewVerbCommandValidator : AbstractValidator<AddNewVerbCommand>
{
    public AddNewVerbCommandValidator()
    {
        RuleFor(c => c.VerbDto.Binyan).NotEmpty();
        RuleFor(c => c.VerbDto.Infinitive.Hebrew).NotEmpty();
    }
    
}
