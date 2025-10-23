using NUnit.Framework;

[TestFixture]
public class MilkTypeTests
{
    [Test]
    public void MilkType_HasExpectedValues()
    {
        Assert.AreEqual(0, (int)MilkType.Dairy);
        Assert.AreEqual(1, (int)MilkType.Almond);
        Assert.AreEqual(2, (int)MilkType.Oat);
        Assert.AreEqual(3, (int)MilkType.None);
    }

    [Test]
    public void MilkType_CanBeCompared()
    {
        MilkType milk1 = MilkType.Dairy;
        MilkType milk2 = MilkType.Dairy;
        MilkType milk3 = MilkType.Almond;

        Assert.AreEqual(milk1, milk2);
        Assert.AreNotEqual(milk1, milk3);
    }

    [Test]
    public void MilkType_CanBeUsedInSwitchStatement()
    {
        MilkType milk = MilkType.Oat;
        string result = "";

        switch (milk)
        {
            case MilkType.Dairy:
                result = "Dairy";
                break;
            case MilkType.Almond:
                result = "Almond";
                break;
            case MilkType.Oat:
                result = "Oat";
                break;
            case MilkType.None:
                result = "None";
                break;
        }

        Assert.AreEqual("Oat", result);
    }

    [Test]
    public void MilkType_DefaultValueIsDairy()
    {
        MilkType defaultMilk = default(MilkType);

        Assert.AreEqual(MilkType.Dairy, defaultMilk);
    }
}