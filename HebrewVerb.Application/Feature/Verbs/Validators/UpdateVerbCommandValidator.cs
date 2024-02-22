using FluentValidation;
using HebrewVerb.Application.Feature.Verbs.Commands;

namespace HebrewVerb.Application.Feature.Verbs.Validators;

public class UpdateVerbCommandValidator : AbstractValidator<UpdateVerbCommand>
{
    public UpdateVerbCommandValidator()
    {
        RuleFor(c => c.VerbDto.Binyan).NotEmpty();
        RuleFor(c => c.VerbDto.Infinitive.Hebrew).NotEmpty();
    }

}
