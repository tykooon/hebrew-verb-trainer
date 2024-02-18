using HebrewVerb.Application.Feature.Verbs;
using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.Application.Common.Extensions;

public static class VerbDtoExtensions
{
    public static WordFormDto? GetWordFormDto(this VerbDto verbDto, Zman zman, Guf guf)
    {
        if (verbDto == null)
        {
            return null;
        }

        if (zman == Zman.Infinitive)
        {
            return verbDto.Infinitive;
        }

        if (zman == Zman.Past)
        {
            return guf.Details() switch
            {
                (_, Number.Single, Gender.Male) => verbDto.PresentMs,
                (_, Number.Plural, Gender.Male) => verbDto.PresentMs,
                (_, Number.Single, Gender.Female) => verbDto.PresentMs,
                (_, Number.Plural, Gender.Female) => verbDto.PresentMs,
                _ => null
            };
        }
        else if (zman == Zman.Past)
        {
            return guf.Details() switch
            {
                (Person.First, Number.Single, _) => verbDto.PastMs1,
                (Person.First, Number.Plural, _) => verbDto.PastMp1,
                (Person.Second, Number.Single, Gender.Male) => verbDto.PastMs2,
                (Person.Second, Number.Single, Gender.Female) => verbDto.PastFs2,
                (Person.Second, Number.Plural, Gender.Male) => verbDto.PastMp2,
                (Person.Second, Number.Plural, Gender.Female) => verbDto.PastFp2,
                (Person.Third, Number.Single, Gender.Male) => verbDto.PastMs3,
                (Person.Third, Number.Single, Gender.Female) => verbDto.PastFs3,
                (Person.Third, Number.Plural, Gender.Male) => verbDto.PastMp3,
                _ => null
            };
        }
        else if (zman == Zman.Future)
        {
            return guf.Details() switch
            {
                (Person.First, Number.Single, _) => verbDto.FutureMs1,
                (Person.First, Number.Plural, _) => verbDto.FutureMp1,
                (Person.Second, Number.Single, Gender.Male) => verbDto.FutureMs2,
                (Person.Second, Number.Single, Gender.Female) => verbDto.FutureFs2,
                (Person.Second, Number.Plural, Gender.Male) => verbDto.FutureMp2,
                (Person.Third, Number.Single, Gender.Male) => verbDto.FutureMs3,
                (Person.Third, Number.Plural, Gender.Male) => verbDto.FutureMp3,
                _ => null
            };
        }
        return guf.Details() switch
        {
            (Person.Second, Number.Single, Gender.Male) => verbDto.ImperativeMs,
            (Person.Second, Number.Single, Gender.Female) => verbDto.ImperativeFs,
            (Person.Second, Number.Plural, Gender.Male) => verbDto.ImperativeMp,
            _ => null
        };
    }

    public static bool HasPresent(this VerbDto verbDto)
    {
        return
            (verbDto.PresentMs != null) ||
            (verbDto.PresentMp != null) ||
            (verbDto.PresentFs != null) ||
            (verbDto.PresentFp != null);
    }

    public static bool HasPast(this VerbDto verbDto)
    {
        return
            (verbDto.PastMs1 != null) ||
            (verbDto.PastMp1 != null) ||
            (verbDto.PastMs2 != null) ||
            (verbDto.PastFs2 != null) ||
            (verbDto.PastMp2 != null) ||
            (verbDto.PastFp2 != null) ||
            (verbDto.PastMs3 != null) ||
            (verbDto.PastFs3 != null) ||
            (verbDto.PastMp3 != null);
    }

    public static bool HasFuture(this VerbDto verbDto)
    {
        return
            (verbDto.FutureMs1 != null) ||
            (verbDto.FutureMp1 != null) ||
            (verbDto.FutureMs2 != null) ||
            (verbDto.FutureFs2 != null) ||
            (verbDto.FutureMp2 != null) ||
            (verbDto.FutureMs3 != null) ||
            (verbDto.FutureMp3 != null);
    }

    public static bool HasImperative(this VerbDto verbDto)
    {
        return
            (verbDto.ImperativeMs != null) ||
            (verbDto.ImperativeMp != null) ||
            (verbDto.ImperativeFs != null);
    }
}
