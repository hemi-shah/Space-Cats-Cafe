using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private TicketBoard ticketBoard;

    [Header("Random Order Weights")] 
    [Range(0f, 1f)] [SerializeField] private float hotChance = 0.5f;

    [Range(0f, 1f)] [SerializeField] private float milkChance = 0.75f;
    [Range(0f, 1f)] [SerializeField] private float whippedChance = 0.75f;
    [Range(0f, 1f)] [SerializeField] private float toppingChance = 0.35f;

    [Header("Ice Settings (iced only")] 
    [SerializeField] private int minIceCubes = 1;
    [SerializeField] private int maxIceCubes = 3;

    private int nextOrderNumber = 1;
    private List<int> activeOrderNumbers = new List<int>();

    // Create an order given specific data
    public int CreateOrder(OrderTicketData data)
    {
        int orderNumber = nextOrderNumber++;
        activeOrderNumbers.Add(orderNumber);

        if (ticketBoard)
        {
            Debug.Log($"[OrderManager] Spawning ticket #{orderNumber}");
            ticketBoard.SpawnTicket(orderNumber, data);
        }
        else
        {
            Debug.LogError("[OrderManager] ticketBoard ref is missing", this);
        }
        
        return orderNumber;
    }
    

    // Complete order and remove ticket
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
            hasChocolateSyrup = false,
            hasCaramelSyrup = true,
            numberOfIceCubes = 2,
            milk = MilkType.Almond,
            syrup = SyrupType.Caramel,
            drinkName = "Iced Caramel Latte"
        };
        CreateOrder(data);
    }

    // test 2
    public void CreateTestOrder2()
    {
        OrderTicketData data = new OrderTicketData
        {
            isHot = true,
            hasWhippedCream = true,
            hasChocolateSyrup = true,
            hasCaramelSyrup = false,
            numberOfIceCubes = 0,
            milk = MilkType.Dairy,
            syrup = SyrupType.Mocha,
            drinkName = "Hot Mocha Latte"
        };
        CreateOrder(data);
    }

    public void CreateRandomTestOrder()
    {
        OrderTicketData data = GenerateRandomOrderData();
        CreateOrder(data);
    }

    public OrderTicketData GenerateRandomOrderData()
    {
        // hot or iced
        bool randIsHot = Random.value < hotChance;
        
        // milk
        bool randHasMilk = Random.value < milkChance;

        MilkType randMilkType = MilkType.None;

        // If drink has milk, randomly choose which type
        if (randHasMilk)
        {
            randMilkType = PickRandomMilk();
        }

        SyrupType randSyrup = PickRandomSyrup();
        
        // randomize number of ice cubes
        int randIce = randIsHot ? 0 : Random.Range(minIceCubes, maxIceCubes + 1);

        // randomize whipped cream
        bool randHasWhippedCream = Random.value < whippedChance;
        
        // if whipped cream, randomize drizzle flavors
        bool randToppingChocolate = randHasWhippedCream && (Random.value < toppingChance);
        bool randToppingCaramel = randHasWhippedCream && (Random.value < toppingChance);
        
        // drink name
        string HotIced = randIsHot ? "Hot" : "Iced";
        string flavor = FlavorFromSyrup(randSyrup);
        string baseName = (randMilkType != MilkType.None) ? "Latte" : "Espresso";
        string drinkName = $"{HotIced} {flavor} {baseName}";

        return new OrderTicketData
        {
            isHot = randIsHot,
            hasWhippedCream = randHasWhippedCream,
            hasChocolateSyrup = randToppingChocolate,
            hasCaramelSyrup = randToppingCaramel,

            numberOfIceCubes = randIce,
            milk = randMilkType,
            syrup = randSyrup,

            drinkName = drinkName,
        };
    }

    private static string FlavorFromSyrup(SyrupType syrup)
    {
        switch (syrup)
        {
            case SyrupType.Caramel:   return "Caramel";
            case SyrupType.Mocha:     return "Mocha";
            case SyrupType.Chocolate: return "Chocolate";
            default:                  return "Classic";
        }
    }

    private static SyrupType PickRandomSyrup()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:  return SyrupType.Caramel;
            case 1:  return SyrupType.Mocha;
            default: return SyrupType.Chocolate;
        }
    }

    private static MilkType PickRandomMilk()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:  return MilkType.Dairy;
            case 1:  return MilkType.Almond;
            default: return MilkType.Oat;
        }
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
