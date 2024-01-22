using FluentValidation;

namespace HebrewVerb.Application.Feature.Gizras.Commands;

public class AddNewGizraCommandValidator : AbstractValidator<AddNewGizraCommand>
{
    public AddNewGizraCommandValidator()
    {
        RuleFor(d => d.Name).NotEmpty();
    }
}
