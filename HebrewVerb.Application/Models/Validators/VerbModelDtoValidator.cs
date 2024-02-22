using FluentValidation;
using HebrewVerb.Application.Feature.VerbModels.Queries;
using MediatR;
using System.Linq;

namespace HebrewVerb.Application.Models.Validators;

public class VerbModelDtoValidator : AbstractValidator<VerbModelDto>
{
    private readonly IMediator _mediator;

    public VerbModelDtoValidator(IMediator mediator)
    {
        _mediator = mediator;

        RuleFor(vm => vm.Name).NotEmpty().WithMessage("Название не может быть пустым");

        RuleFor(vm => vm).MustAsync(async (dto, cancellationToken) => await IsUniqueAsync(dto))
            .WithMessage("Данное название уже использовано");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<VerbModelDto>.CreateWithOptions((VerbModelDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };

    private async Task<bool> IsUniqueAsync(VerbModelDto dto)
    {
        var res = await _mediator.Send(new GetAllVerbModelsQuery());
        return !res.Any(vm => vm.Id != dto.Id && vm.Name == dto.Name);
    }
}
