using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private TicketBoard ticketBoard;

    private int nextOrderNumber = 1;

    private List<int> activeOrderNumbers = new List<int>();

    public int CreateOrder(OrderTicketData data)
    {
        int orderNumber = nextOrderNumber++;
        activeOrderNumbers.Add(orderNumber);

        if (ticketBoard)
        {
            ticketBoard.SpawnTicket(orderNumber, data);
        }
        
        return orderNumber;
    }

    public void CompleteOrder(int orderNumber)
    {
        activeOrderNumbers.Remove(orderNumber);
        if (ticketBoard) ticketBoard.RemoveTicket(orderNumber);
    }
    
    // test
    public void CreateTestOrder()
    {
        OrderTicketData data = new OrderTicketData
        {
            isHot = false,
            hasWhippedCream = true,
            hasChocolateSyrup = true,
            hasCaramelSyrup = false,
            numberOfIceCubes = 2,
            milk = MilkType.Almond,
            syrup = SyrupType.Caramel,
            drinkName = "Iced Caramel Latte"
        };
        CreateOrder(data);
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
