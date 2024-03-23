using FluentValidation;
using HebrewVerb.Application.Feature.Shoreshes.Queries;

namespace HebrewVerb.Application.Feature.Shoreshes.Validators;

public class GetShoreshByIdQueryValidator : AbstractValidator<GetShoreshByIdQuery>
{
    public GetShoreshByIdQueryValidator()
    {
        RuleFor(r => r.ShoreshId).GreaterThan(0);
    }
}
