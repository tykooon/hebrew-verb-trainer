using HebrewVerb.Domain.Entities;

namespace HebrewVerb.DomainUnitTests;

public class ShoreshTest
{
    [Fact]
    public void Shoresh3CtorTest()
    {
        var sh = new Shoresh("קרב");
        Assert.Equal('ק', sh.First);
        Assert.Equal('ר', sh.Second);
        Assert.Equal('ב', sh.Third);
        Assert.Null(sh.Fourth);
        Assert.False(sh.IsLong);
    }

    [Fact]
    public void Shoresh4CtorTest()
    {
        var sh = new Shoresh("עצבן");
        Assert.Equal('ע', sh.First);
        Assert.Equal('צ', sh.Second);
        Assert.Equal('ב', sh.Third);
        Assert.NotNull(sh.Fourth);
        Assert.True(sh.IsLong);

    }
}