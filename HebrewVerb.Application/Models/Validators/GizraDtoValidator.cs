using FluentValidation;
using HebrewVerb.Application.Feature.Gizras.Queries;
using MediatR;

namespace HebrewVerb.Application.Models.Validators;

public class GizraDtoValidator : AbstractValidator<GizraDto>
{
    private readonly IMediator _mediator;

    public GizraDtoValidator(IMediator mediator)
    {
        _mediator = mediator;

        RuleFor(g => g.Name).NotEmpty().WithMessage("Название не может быть пустым");

        RuleFor(g => g).MustAsync(async (dto, cancellationToken) => await IsUniqueAsync(dto))
            .WithMessage("Данное название уже использовано");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<GizraDto>.CreateWithOptions((GizraDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };

    private async Task<bool> IsUniqueAsync(GizraDto dto)
    {
        var res = await _mediator.Send(new GetAllGizrasQuery());
        return !res.Any(g => g.Id != dto.Id && g.Name == dto.Name);
    }
}
