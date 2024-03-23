using HebrewVerb.SharedKernel.Abstractions;
using HebrewVerb.SharedKernel.Enums;
using HebrewVerb.SharedKernel.Extensions;

namespace HebrewVerb.SharedKernel.UnitTests;

public class HebrewVerbTagEnumTest
{
    [Fact]
    public void ZmanTest()
    {
        foreach(var item in Zman.List)
        {
            var valueCalc = 1;
            for(int i = 0; i < item.Id; i++)
            {
                valueCalc <<= 1;
            }
            Assert.Equal(valueCalc, item.Flag);
        }
    }

    [Fact]
    public void ZmanListTest()
    {
        Assert.Equal(5, Zman.List.Count);
        Assert.Equal(2 + 4 + 8, HebrewTagFlagExtensions.GetFlagSum([Zman.Past, Zman.Present, Zman.Future]));
        Assert.True(Zman.Present.IsIncluded(7));
        Assert.False(Zman.Imperative.IsIncluded(15));

        Assert.Equal("עבר", Zman.Past.ToString(Language.Hebrew));
    }
}