using FluentValidation;
using HebrewVerb.Application.Feature.Shoreshes.Queries;

namespace HebrewVerb.Application.Feature.Shoreshes.Validators;

public class GetShoreshByNameQueryValidator : AbstractValidator<GetShoreshByNameQuery>
{
    public GetShoreshByNameQueryValidator()
    {
        RuleFor(r => r.ShoreshName.Length).GreaterThanOrEqualTo(3).LessThanOrEqualTo(4);
        RuleFor(r => r.ShoreshName).Matches("^[אבגדהוזטחיכךמםנןסעפףצץקרשת]{3,4}$");
    }
}
