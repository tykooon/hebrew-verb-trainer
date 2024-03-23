using FluentValidation;
using HebrewVerb.Application.Common.Helpers;
using HebrewVerb.Application.Feature.Verbs.Queries;
using MediatR;

namespace HebrewVerb.Application.Models.Validators;

public class VerbDtoValidator : AbstractValidator<VerbDto>
{
    private readonly IMediator _mediator;

    public VerbDtoValidator(IMediator mediator)
    {
        _mediator = mediator;

        RuleFor(vm => vm.Infinitive).NotEmpty().WithMessage("Инфинитив не может быть пустым");

        RuleFor(vm => vm.Binyan).NotEmpty().WithMessage("Биньян не может быть пустым");

        RuleFor(vm => vm.Shoresh).NotEmpty().WithMessage("Корень не может быть пустым");

        RuleFor(v => v.Shoresh.RemoveNonHebrew(false).Length).GreaterThanOrEqualTo(3).LessThanOrEqualTo(4)
            .WithMessage("Корень - 3 или 4 буквы без точек и огласовок");

        RuleFor(v => v).MustAsync(async (dto, cancellationToken) => await IsUniqueAsync(dto))
            .WithMessage("Данный глагол уже существует");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<VerbDto>.CreateWithOptions((VerbDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };

    private async Task<bool> IsUniqueAsync(VerbDto dto)
    {
        var filter = Filter.FromParams([dto.Binyan], [], [], [], [], int.MaxValue);
        var res = await _mediator.Send(new GetVerbInfosByFilterQuery(filter)) ?? [];
        return !res.Any(v => v.VerbId != dto.Id && v.Infinitive == dto.Infinitive.Hebrew);
    }
}
