﻿namespace HebrewVerb.Application.Models;

public class BinyanDto
{
    public int? Id { get; set; } = null;
    public string Name { get; set; } = "";
    public string NameHebrew { get; set; } = "";
    public string NameLocal { get; set; } = "";
    public bool IsActive { get; set; } = true;

    public override string ToString() => NameLocal;

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not BinyanDto dto) return false;
        if (ReferenceEquals(this, obj)) return true;
        return (Id == dto.Id);
    }
     public override int GetHashCode() => Id?.GetHashCode() ?? -1;

}
