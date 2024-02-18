using HebrewVerb.Domain.Entities;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Domain.Interfaces;

public interface IConjugation
{
    WordForm? Conjugate(Guf guf);
}
