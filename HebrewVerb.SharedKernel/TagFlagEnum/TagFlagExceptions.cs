namespace HebrewVerb.SharedKernel.TagFlagEnum;

public class InvalidTagFlagIdException : Exception
{
    public InvalidTagFlagIdException() : base()
    {
    }

    public InvalidTagFlagIdException(string message) : base(message)
    {
    }

    public InvalidTagFlagIdException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
