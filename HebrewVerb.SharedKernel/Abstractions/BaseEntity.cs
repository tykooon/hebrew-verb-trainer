namespace HebrewVerb.SharedKernel.Abstractions;

public abstract class BaseEntity<TKey> where TKey : struct
{
    public TKey Id { get; private set; }

}
