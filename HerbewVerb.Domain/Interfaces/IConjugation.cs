using HebrewVerb.Domain.Entities;
using HebrewVerb.Domain.Enums;

namespace HebrewVerb.Domain.Interfaces;

public interface IConjugation
{
    WordForm? Conjugate(Guf guf);
}
