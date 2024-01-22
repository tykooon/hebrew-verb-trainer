namespace HebrewVerb.Domain.Exceptions;

public class DomainException(string errorMessage) : Exception(errorMessage)
{
}
