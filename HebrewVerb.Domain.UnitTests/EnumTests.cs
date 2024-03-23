using HebrewVerb.SharedKernel.Enums;

namespace HebrewVerb.DomainUnitTests;

public class EnumTests
{
    [Fact]
    public void GufFromValueTest()
    {
        Assert.Equal(Guf.MS1, Guf.FromId(0b0100));
        Assert.Equal(Guf.FS1, Guf.FromId(0b0101));
        Assert.Equal(Guf.MP1, Guf.FromId(0b0110));
        Assert.Equal(Guf.FP1, Guf.FromId(0b0111));
        Assert.Equal(Guf.MS2, Guf.FromId(0b1000));
        Assert.Equal(Guf.FS2, Guf.FromId(0b1001));
        Assert.Equal(Guf.MP2, Guf.FromId(0b1010));
        Assert.Equal(Guf.FP2, Guf.FromId(0b1011));
        Assert.Equal(Guf.MS3, Guf.FromId(0b1100));
        Assert.Equal(Guf.FS3, Guf.FromId(0b1101));
        Assert.Equal(Guf.MP3, Guf.FromId(0b1110));
        Assert.Equal(Guf.FP3, Guf.FromId(0b1111));

        Assert.Equal(Guf.Undefined, Guf.FromId(0b0000));

    }

    [Fact]
    public void GufFromEnumsTest()
    {
        Assert.Equal(Guf.MS1, Guf.From(Person.First, Number.Single, Gender.Male));
        Assert.Equal(Guf.Undefined, Guf.From(Person.Impersonal, Number.Single, Gender.Male));
    }

    [Fact]
    public void GufDetailsTest()
    {
        Assert.Equal((Person.Third, Number.Plural, Gender.Female), Guf.FP3.Details());
    
    }
}