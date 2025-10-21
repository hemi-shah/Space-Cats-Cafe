using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CatSelectionManager : MonoBehaviour
{
    [Header("Cat References")]
    [SerializeField] private CatCatalog catCatalog;
    [SerializeField] private string playerTag = "PlayerCat";
    
    [SerializeField] private string currentCatType;
    [SerializeField] private Image playerCatImage;
    
    [Header("UI References")] 
    [SerializeField] private Button asteroidCatButton;
    [SerializeField] private Button neptuneCatButton;

    // Static reference to easily access the current player cat type from other scripts
    public static string CurrentPlayerCatType { get; private set; }

    private void Start()
    {
        FindPlayerCat();
        
        // Set up button click listeners
        if (asteroidCatButton != null)
        {
            asteroidCatButton.onClick.AddListener(() => SelectCat("AsteroidCat"));
        }

        if (neptuneCatButton != null)
        {
            neptuneCatButton.onClick.AddListener(() => SelectCat("NeptuneCat"));
        }

        // Set default cat if none selected
        if (string.IsNullOrEmpty(currentCatType) && playerCatImage != null)
        {
            SelectCat("AsteroidCat");
        }
    }

    public void SelectCat(string catType)
    {
        Debug.Log($"=== SELECT CAT: {catType} ===");
        
        // Double-check we have the player cat
        if (playerCatImage == null)
        {
            FindPlayerCat();
        }
        
        if (playerCatImage == null)
        {
            Debug.LogError("Player cat image is still null!");
            return;
        }

        currentCatType = catType;
        CurrentPlayerCatType = catType; // Update static reference
        
        CatDefinition selectedCat = FindPlayerCatDefinition(catType);

        if (selectedCat != null)
        {
            playerCatImage.sprite = selectedCat.catSprite;
            Debug.Log($"Selected cat: {catType} for GameObject: {playerCatImage.gameObject.name}");
            
            // Verify the sprite was actually set
            if (playerCatImage.sprite == selectedCat.catSprite)
            {
                Debug.Log($"✅ Sprite successfully assigned: {playerCatImage.sprite.name}");
            }
            else
            {
                Debug.LogError($"❌ Sprite assignment failed!");
            }
        }
        else
        {
            Debug.LogError($"Could not find cat type: {catType}");
        }
    }

    // For player cats only
    private CatDefinition FindPlayerCatDefinition(string catName)
    {
        if (catCatalog == null || catCatalog.playerCats == null)
        {
            Debug.LogError("CatCatalog or playerCats is not assigned or is empty!");
            return null;
        }

        foreach (CatDefinition cat in catCatalog.playerCats)
        {
            if (cat.catName == catName)
            {
                return cat;
            }
        }

        Debug.LogError($"Player cat '{catName}' not found in catalog!");
        return null;
    }

    // For customer cats only
    public CatDefinition GetRandomCustomerCat()
    {
        if (catCatalog == null || catCatalog.customerCats == null || catCatalog.customerCats.Count == 0)
        {
            Debug.LogError("CatCatalog or customerCats is not assigned or is empty!");
            return null;
        }

        int randomIndex = Random.Range(0, catCatalog.customerCats.Count);
        return catCatalog.customerCats[randomIndex];
    }

    private void FindPlayerCat()
    {
        GameObject playerCatObject = GameObject.FindGameObjectWithTag(playerTag);
    
        if (playerCatObject != null)
        {
            playerCatImage = playerCatObject.GetComponent<Image>();
            if (playerCatImage != null)
            {
                Debug.Log($"✅ Found player cat: {playerCatObject.name} with Image component");
            }
            else
            {
                Debug.LogError($"Found GameObject with tag '{playerTag}' but no Image component: {playerCatObject.name}");
                
                // Try to find Image in children
                playerCatImage = playerCatObject.GetComponentInChildren<Image>();
                if (playerCatImage != null)
                {
                    Debug.Log($"✅ Found Image component in children: {playerCatImage.gameObject.name}");
                }
            }
        }
        else
        {
            Debug.LogError($"❌ No GameObject found with tag '{playerTag}'");
            
            // Debug: List all objects with tags
            Debug.Log("All tagged objects in scene:");
            GameObject[] allTaggedObjects = GameObject.FindGameObjectsWithTag("PlayerCat");
            foreach (GameObject obj in allTaggedObjects)
            {
                Debug.Log($" - {obj.name}");
            }
        }
    }

    public string GetCurrentCatType()
    {
        return currentCatType;
    }

    // Static method for easy access from customer generators
    public static bool IsPlayerCat(string catName)
    {
        return catName == CurrentPlayerCatType;
    }
}