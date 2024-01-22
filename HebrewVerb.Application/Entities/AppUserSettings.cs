using HebrewVerb.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace HebrewVerb.Application.Entities;

public class AppUserSettings : BaseEntity<int>
{
    public static readonly AppUserSettings Default = new();

    //public CultureInfo Culture { get; private set; } = new CultureInfo("ru-ru");

    //Add Properties for 

    [ForeignKey("AppUser")]
    public int AppUserId { get; private set; }

    //[InverseProperty("AppUserSettings")]
    public AppUser AppUser { get; private set; } = new();
}
