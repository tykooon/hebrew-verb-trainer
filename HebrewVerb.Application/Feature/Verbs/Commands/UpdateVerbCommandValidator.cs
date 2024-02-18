using FluentValidation;

namespace HebrewVerb.Application.Feature.Verbs.Commands;

public class UpdateVerbCommandValidator : AbstractValidator<UpdateVerbCommand>
{
    public UpdateVerbCommandValidator()
    {
        RuleFor(c => c.VerbDto.Binyan).NotEmpty();
        RuleFor(c => c.VerbDto.Infinitive.Hebrew).NotEmpty();
    }
    
}
