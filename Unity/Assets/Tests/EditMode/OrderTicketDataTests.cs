using NUnit.Framework;

[TestFixture]
public class OrderTicketDataTests
{
    [Test]
    public void OrderTicketData_CanBeInstantiated()
    {
        var orderData = new OrderTicketData();

        Assert.IsNotNull(orderData);
    }

    [Test]
    public void OrderTicketData_HotDrink_HasZeroIceCubes()
    {
        var orderData = new OrderTicketData
        {
            isHot = true,
            numberOfIceCubes = 0
        };

        Assert.IsTrue(orderData.isHot);
        Assert.AreEqual(0, orderData.numberOfIceCubes);
    }

    [Test]
    public void OrderTicketData_ColdDrink_CanHaveIceCubes()
    {
        var orderData = new OrderTicketData
        {
            isHot = false,
            numberOfIceCubes = 3
        };

        Assert.IsFalse(orderData.isHot);
        Assert.AreEqual(3, orderData.numberOfIceCubes);
    }

    [Test]
    public void OrderTicketData_AllPropertiesCanBeSet()
    {
        var orderData = new OrderTicketData
        {
            isHot = false,
            hasWhippedCream = true,
            hasChocolateSyrup = true,
            hasCaramelSyrup = false,
            numberOfIceCubes = 2,
            milk = MilkType.Almond,
            syrup = SyrupType.Mocha,
            drinkName = "Iced Mocha with Almond Milk"
        };

        Assert.IsFalse(orderData.isHot);
        Assert.IsTrue(orderData.hasWhippedCream);
        Assert.IsTrue(orderData.hasChocolateSyrup);
        Assert.IsFalse(orderData.hasCaramelSyrup);
        Assert.AreEqual(2, orderData.numberOfIceCubes);
        Assert.AreEqual(MilkType.Almond, orderData.milk);
        Assert.AreEqual(SyrupType.Mocha, orderData.syrup);
        Assert.AreEqual("Iced Mocha with Almond Milk", orderData.drinkName);
    }

    [Test]
    public void OrderTicketData_DrinkNameCanBeNull()
    {
        var orderData = new OrderTicketData
        {
            drinkName = null
        };

        Assert.IsNull(orderData.drinkName);
    }

    [Test]
    public void OrderTicketData_MultipleInstances_AreIndependent()
    {
        var order1 = new OrderTicketData { drinkName = "Latte" };
        var order2 = new OrderTicketData { drinkName = "Cappuccino" };

        Assert.AreNotEqual(order1.drinkName, order2.drinkName);
        Assert.AreEqual("Latte", order1.drinkName);
        Assert.AreEqual("Cappuccino", order2.drinkName);
    }
}