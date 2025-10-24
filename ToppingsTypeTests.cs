using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class ToppingsTypeTests
{
    [Test]
    public void ToppingsType_HasExpectedValues()
    {
        Assert.AreEqual(0, (int)ToppingsType.WhippedCream);
        Assert.AreEqual(1, (int)ToppingsType.ChocolateSyrup);
        Assert.AreEqual(2, (int)ToppingsType.CaramelSyrup);
        Assert.AreEqual(3, (int)ToppingsType.None);
    }

    [Test]
    public void ToppingsType_CanBeCompared()
    {
        ToppingsType topping1 = ToppingsType.WhippedCream;
        ToppingsType topping2 = ToppingsType.WhippedCream;
        ToppingsType topping3 = ToppingsType.CaramelSyrup;

        Assert.AreEqual(topping1, topping2);
        Assert.AreNotEqual(topping1, topping3);
    }

    [Test]
    public void ToppingsType_CanBeUsedInSwitchStatement()
    {
        ToppingsType topping = ToppingsType.ChocolateSyrup;
        bool result = false;

        switch (topping)
        {
            case ToppingsType.WhippedCream:
                result = false;
                break;
            case ToppingsType.ChocolateSyrup:
                result = true;
                break;
            case ToppingsType.CaramelSyrup:
                result = false;
                break;
            case ToppingsType.None:
                result = false;
                break;
        }

        Assert.IsTrue(result);
    }

    [Test]
    public void ToppingsType_DefaultValueIsWhippedCream()
    {
        ToppingsType defaultTopping = default(ToppingsType);

        Assert.AreEqual(ToppingsType.WhippedCream, defaultTopping);
    }

    [Test]
    public void ToppingsType_AllValuesAreUnique()
    {
        var values = new[] { 
            ToppingsType.WhippedCream, 
            ToppingsType.ChocolateSyrup, 
            ToppingsType.CaramelSyrup, 
            ToppingsType.None 
        };

        var uniqueValues = new HashSet<ToppingsType>(values);

        Assert.AreEqual(4, uniqueValues.Count);
    }
}