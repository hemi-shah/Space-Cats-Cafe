using System;
using System.Collections.Generic;
using UnityEngine;

public class CatCollectionManager : MonoBehaviour
{
    [Header("Collected Cats")]
    [SerializeField] private List<CatDefinition> collectedCats = new List<CatDefinition>();

    // ADD THIS EVENT
    public static event Action OnCollectionChanged;

    // Singleton for easy access
    public static CatCollectionManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("CatCollectionManager Awake called");
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CatCollectionManager instance created and set as singleton");
        }
        else
        {
            Debug.Log("Duplicate CatCollectionManager destroyed");
            Destroy(gameObject);
        }
    }

    public void AddCatToCollection(CatDefinition cat)
    {
        if (cat == null) 
        {
            Debug.LogError("Tried to add null cat to collection!");
            return;
        }
        
        // Check if cat is already collected
        if (!collectedCats.Contains(cat))
        {
            collectedCats.Add(cat);
            Debug.Log($"✅ SUCCESS: Added {cat.catName} to collection! Total: {collectedCats.Count}");
            
            // ADD THIS LINE: Trigger the event when a cat is added
            OnCollectionChanged?.Invoke();
            
            // Debug: List all collected cats
            Debug.Log("Current collection:");
            foreach (var collectedCat in collectedCats)
            {
                Debug.Log($" - {collectedCat.catName}");
            }
        }
        else
        {
            Debug.Log($"ℹ️ {cat.catName} is already in collection");
        }
    }

    public List<CatDefinition> GetCollectedCats()
    {
        return new List<CatDefinition>(collectedCats);
    }

    public bool HasCat(CatDefinition cat)
    {
        return collectedCats.Contains(cat);
    }

    public int GetCollectionCount()
    {
        return collectedCats.Count;
    }
}