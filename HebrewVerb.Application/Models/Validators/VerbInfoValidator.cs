using FluentValidation;
using HebrewVerb.Application.Common.Helpers;

namespace HebrewVerb.Application.Models.Validators;

public class VerbInfoValidator : AbstractValidator<VerbInfo>
{
    public VerbInfoValidator()
    {
        RuleFor(vm => vm.Infinitive).NotEmpty().WithMessage("Инфинитив не может быть пустым");

        RuleFor(vm => vm.Binyan).NotEmpty().WithMessage("Биньян не может быть пустым");

        RuleFor(vm => vm.Shoresh).NotEmpty().WithMessage("Корень не может быть пустым");

        RuleFor(v => v.Shoresh.RemoveNonHebrew(false).Length).GreaterThanOrEqualTo(3).LessThanOrEqualTo(4)
            .WithMessage("Корень - 3 или 4 буквы без точек и огласовок");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<VerbInfo>.CreateWithOptions((VerbInfo)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
