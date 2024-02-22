using FluentValidation;
using HebrewVerb.Application.Feature.Gizras.Commands;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Feature.Gizras.Validators;

public class UpdateGizraCommandValidator : AbstractValidator<UpdateGizraCommand>
{
    public UpdateGizraCommandValidator()
    {
        RuleFor(d => d.GizraDto.Name).NotEmpty();
        RuleFor(d => d.GizraDto.Binyans)
            .ForEach(b => b.Must(b => Binyan.TryFromName(b, out _)));
    }
}
