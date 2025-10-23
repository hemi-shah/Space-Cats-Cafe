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

    public int TakeOrderForCat(CatDefinition cat)
    {
        // Add cat to collection immediately when order is taken
        AddCatToCollection(cat);

        // generate random order
        var data = orderManager.GenerateRandomOrderData();
        int orderNum = orderManager.CreateOrder(data);

        sessions[orderNum] = new CustomerSession
        {
            cat = cat,
            orderNumber = orderNum,
            orderData = data
        };

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

    // NEW: Add cat to collection
    private void AddCatToCollection(CatDefinition cat)
    {
        if (cat == null) return;

        // Try to find the CatCollectionManager
        CatCollectionManager collectionManager = FindFirstObjectByType<CatCollectionManager>();
        
        // If it doesn't exist, create one
        if (collectionManager == null)
        {
            GameObject collectionObj = new GameObject("CatCollectionManager");
            collectionManager = collectionObj.AddComponent<CatCollectionManager>();
            DontDestroyOnLoad(collectionObj);
        }

        // Add the cat to collection
        collectionManager.AddCatToCollection(cat);
        Debug.Log($"📸 Added {cat.catName} to cat collection!");
    }
    public int GetSessionCount()
    {
        return sessions.Count;
    }
}