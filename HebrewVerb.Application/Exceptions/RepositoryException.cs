namespace HebrewVerb.Application.Exceptions;

[Serializable]
public class RepositoryException : Exception
{
    public List<string> Errors { get; } = [];

    public RepositoryException(string errorMessage) : base(errorMessage)
    {
    }

    public RepositoryException(string errorMessage, Exception innerException) :
        base(errorMessage, innerException)
    {
    }
}