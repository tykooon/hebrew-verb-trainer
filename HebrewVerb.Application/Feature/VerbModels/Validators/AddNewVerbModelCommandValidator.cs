using FluentValidation;
using HebrewVerb.Application.Feature.VerbModels.Commands;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Feature.VerbModels.Validators;

public class AddNewVerbModelCommandValidator : AbstractValidator<AddNewVerbModelCommand>
{
    public AddNewVerbModelCommandValidator()
    {
        RuleFor(d => d.Name).NotEmpty();
        RuleFor(d => d.Binyans)
            .ForEach(b => b.Must(b => Binyan.TryFromName(b, out _)));
    }
}
