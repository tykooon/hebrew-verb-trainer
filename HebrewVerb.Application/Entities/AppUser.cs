﻿using HebrewVerb.Application.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace HebrewVerb.Application.Entities;

public class AppUser : IdentityUser<int>
{
    public static readonly AppUser Anonymous = new();

    public AppUserStatus Status { get; set; } = AppUserStatus.Basic;

    public AppUserSettings Settings { get; set; } = AppUserSettings.Default;

    public ICollection<AppFilter> Filters { get; set; } = [];
}
