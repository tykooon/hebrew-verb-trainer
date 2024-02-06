using FluentValidation;

namespace HebrewVerb.Application.Feature.Shoreshes.Queries;

public class GetShoreshByIdQueryValidator : AbstractValidator<GetShoreshByIdQuery>
{
    public GetShoreshByIdQueryValidator()
    {
        RuleFor(r => r.ShoreshId).GreaterThan(0);
    }
}
