using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.SharedKernel.Abstractions;

public interface IHebrewTagFlag
{
    string Name { get; }
    int Id { get; }
    int Flag { get; }

    string NameHebrew { get; }
    string NameRussian { get; }
    string NameEnglish { get; }

    string ToString(Language lang);
}
