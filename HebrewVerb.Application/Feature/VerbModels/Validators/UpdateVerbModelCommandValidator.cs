using FluentValidation;
using HebrewVerb.Application.Feature.VerbModels.Commands;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Feature.VerbModels.Validators;

public class UpdateVerbModelCommandValidator : AbstractValidator<UpdateVerbModelCommand>
{
    public UpdateVerbModelCommandValidator()
    {
        RuleFor(d => d.VerbModelDto.Name).NotEmpty();
        RuleFor(d => d.VerbModelDto.Binyans)
            .ForEach(b => b.Must(b => Binyan.TryFromName(b, out _)));
    }
}
