namespace HebrewVerb.Domain.Common;

public abstract class BaseEntity<TKey> where TKey : struct
{
    public TKey Id { get; private set; }

}
