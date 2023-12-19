﻿namespace HebrewVerb.Core;

public abstract class Entity<TKey> where TKey : struct
{
    public TKey Id { get; set; }
}
