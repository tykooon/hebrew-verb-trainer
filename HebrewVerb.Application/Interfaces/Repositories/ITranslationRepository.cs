﻿using HebrewVerb.Domain.Entities;

namespace HebrewVerb.Application.Interfaces.Repositories;

public interface ITranslationRepository : IRepository<Translation, int>
{
    // IEnumerable<Translation> GetVerbTranslations(int verbId);
}
