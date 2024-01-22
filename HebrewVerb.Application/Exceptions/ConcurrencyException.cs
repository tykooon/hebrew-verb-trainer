namespace HebrewVerb.Application.Exceptions;

[Serializable]
public class ConcurrencyException : RepositoryException
{
    public ConcurrencyException(IEnumerable<string> errors) :
        base("Some properties break optimistic concurrency.")
    {
        Errors.AddRange(errors);
    }
}