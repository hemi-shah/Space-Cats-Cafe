using NUnit.Framework;

[TestFixture]
public class StationTypeTests
{
    [Test]
    public void StationType_HasExpectedValues()
    {
        Assert.AreEqual(0, (int)StationType.DrinkSelection);
        Assert.AreEqual(1, (int)StationType.IceStation);
        Assert.AreEqual(2, (int)StationType.SyrupSelection);
        Assert.AreEqual(3, (int)StationType.EspressoStation);
        Assert.AreEqual(4, (int)StationType.MilkStation);
        Assert.AreEqual(5, (int)StationType.Completed);
    }

    [Test]
    public void StationType_CanBeCompared()
    {
        StationType station1 = StationType.IceStation;
        StationType station2 = StationType.IceStation;
        StationType station3 = StationType.MilkStation;

        Assert.AreEqual(station1, station2);
        Assert.AreNotEqual(station1, station3);
    }

    [Test]
    public void StationType_CanBeUsedInWorkflow()
    {
        StationType current = StationType.DrinkSelection;
        StationType next = StationType.IceStation;

        bool canAdvance = (int)current < (int)next;

        Assert.IsTrue(canAdvance);
    }

    [Test]
    public void StationType_CompletedIsLastStation()
    {
        var allStations = new[] {
            StationType.DrinkSelection,
            StationType.IceStation,
            StationType.SyrupSelection,
            StationType.EspressoStation,
            StationType.MilkStation,
            StationType.Completed
        };

        int maxValue = (int)StationType.Completed;

        foreach (var station in allStations)
        {
            Assert.LessOrEqual((int)station, maxValue);
        }
    }

    [Test]
    public void StationType_DefaultValueIsDrinkSelection()
    {
        StationType defaultStation = default(StationType);

        Assert.AreEqual(StationType.DrinkSelection, defaultStation);
    }
}