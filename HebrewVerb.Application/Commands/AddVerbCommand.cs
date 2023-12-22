using HebrewVerb.Application.Abstractions;
using HebrewVerb.Core;

namespace HebrewVerb.Application.Commands;

public class AddVerbCommand : ICommand<Verb>
{
    public Verb NewVerb { get; }

    public AddVerbCommand(Verb verb)
    {
        NewVerb = verb;
    }
}
