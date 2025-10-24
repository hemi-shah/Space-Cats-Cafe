using NUnit.Framework;

[TestFixture]
public class CustomerSessionTests
{
    [Test]
    public void NewSession_HasDefaultInProgressState()
    {
        var session = new CustomerSession();
        Assert.AreEqual("In Progress", session.state);
    }

    [Test]
    public void Session_CanStoreCatDefinition()
    {
        var session = new CustomerSession();
        var mockCat = new CatDefinition(); // This will have Unity dependencies

        session.cat = mockCat;

        Assert.IsNotNull(session.cat);
        Assert.AreEqual(mockCat, session.cat);
    }

    [Test]
    public void Session_CanStoreOrderNumber()
    {
        var session = new CustomerSession();

        session.orderNumber = 42;

        Assert.AreEqual(42, session.orderNumber);
    }

    [Test]
    public void Session_CanStoreOrderData()
    {
        var session = new CustomerSession();
        var orderData = new OrderTicketData
        {
            isHot = true,
            drinkName = "Hot Latte"
        };

        session.orderData = orderData;

        Assert.IsNotNull(session.orderData);
    }

    [Test]
    public void Session_StateCanBeChanged()
    {
        var session = new CustomerSession();

        session.state = "Completed";

        Assert.AreEqual("Completed", session.state);
    }

    [Test]
    public void Session_CanHaveNullCat()
    {
        var session = new CustomerSession
        {
            cat = null,
            orderNumber = 1
        };

        Assert.IsNull(session.cat);
        Assert.AreEqual(1, session.orderNumber);
    }

    [Test]
    public void Session_CanHaveNullOrderData()
    {
        var session = new CustomerSession
        {
            orderData = null,
            orderNumber = 99
        };

        Assert.IsNull(session.orderData);
        Assert.AreEqual(99, session.orderNumber);
    }
}