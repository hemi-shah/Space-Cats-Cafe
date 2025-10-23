using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private SeatingManager seatingManager;

    private readonly Dictionary<int, CustomerSession> sessions = new();
    
    public static CustomerManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    //private readonly Dictionary<int, CustomerSession> sessions = new Dictionary<int, CustomerSession>();

    public int TakeOrderForCat(CatDefinition cat)
    {
        // generate random order
        var data = orderManager.GenerateRandomOrderData();
        int orderNum = orderManager.CreateOrder(data);

        sessions[orderNum] = new CustomerSession
        {
            cat = cat,
            orderNumber = orderNum,
            orderData = data
        };

        //Debug.Log($"[CustomerManager] Session stored for order {orderNum} (cat={cat.catName})");

        return orderNum;
    }
    
    public bool TryGetSession(int orderNumber, out CustomerSession session) 
        => sessions.TryGetValue(orderNumber, out session);

    public void CompleteOrderAndRemove(int orderNumber)
    {
        orderManager.CompleteOrder(orderNumber);
        sessions.Remove(orderNumber);
        
        if (seatingManager) seatingManager.OnOrderCompleted(orderNumber);
    }


    /*
    public static CustomerManager Instance { get; private set; }

    [Header("Customers in this scene")]
    [SerializeField] private CustomerState[] customers;

    public int ActiveIndex { get; private set; } = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public int Count => customer?.Length ?? 0;

    public void SetActiveCustomer(int index)
    {
        if (index < 0 || index >= Count) { Debug.LogWarning("Bad customer index"); return; }
        ActiveIndex = index;
    }

    public CustomerState GetActiveCustomer()
    {
        if (ActiveIndex < 0 || ActiveIndex >= Count) return null;
        return customers[ActiveIndex];
    }

    public CustomerState GetCustomer(int index)
    {
        if (index < 0 || index >= Count) return null;
        return customers[index];
    }

    public void AssignOrderToActive(int orderId)
    {
        var c = GetActiveCustomer();
        if (c == null) return;
        c.orderId = orderId;
        c.hasOrder = true;
    }

    public bool MarkServedActive()
    {
        var c = GetActiveCustomer();
        if (c == null || !c.hasOrder) return false;
        c.served = true;
        return true;
    }

    public bool IsActiveServed()
    {
        var c = GetActiveCustomer();
        return c != null && c.served;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    */
}
