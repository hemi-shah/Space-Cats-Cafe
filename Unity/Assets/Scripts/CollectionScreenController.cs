using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollectionScreenController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform catGridParent;
    [SerializeField] private GameObject catEntryPrefab;
    [SerializeField] private CanvasGroup canvasGroup;

    private bool isRefreshing = false;

    private void OnEnable()
    {
        Debug.Log("🖼️ Collection Screen Enabled");
        CatCollectionManager.OnCollectionChanged += OnCollectionChanged;
        
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
        // Use a small delay to prevent multiple rapid refreshes
        Invoke(nameof(SafeRefresh), 0.1f);
        
    }

    private void SafeRefresh()
    {
        if (!isRefreshing)
        {
            RefreshCollectionDisplay();
        }
    }

    private void RefreshCollectionDisplay()
    {
        if (isRefreshing) return;
        
        isRefreshing = true;
        Debug.Log("🔄 Refreshing Collection Display");
        
        if (catGridParent == null)
        {
            Debug.LogError("❌ Cat Grid Parent is null!");
            isRefreshing = false;
            return;
        }

        if (catEntryPrefab == null)
        {
            Debug.LogError("❌ Cat Entry Prefab is null!");
            isRefreshing = false;
            return;
        }

        // Clear existing entries
        foreach (Transform child in catGridParent)
        {
            Destroy(child.gameObject);
        }

        // Get collected cats from CatCollectionManager
        CatCollectionManager collectionManager = FindObjectOfType<CatCollectionManager>();
        if (collectionManager == null)
        {
            Debug.LogError("❌ CatCollectionManager not found!");
            isRefreshing = false;
            return;
        }

        List<CatDefinition> collectedCats = collectionManager.GetCollectedCats();
        Debug.Log($"📊 Displaying {collectedCats.Count} collected cats");

        // Create UI entries for each collected cat - limit debug output
        foreach (CatDefinition cat in collectedCats)
        {
            if (cat != null)
            {
                CreateCatEntry(cat);
            }
        }

        // Force UI to update
        LayoutRebuilder.ForceRebuildLayoutImmediate(catGridParent);
        Canvas.ForceUpdateCanvases();
        
        isRefreshing = false;
        Debug.Log("✅ Collection display refresh complete");
    }

    private void CreateCatEntry(CatDefinition cat)
{
    GameObject entry = Instantiate(catEntryPrefab, catGridParent);
    entry.name = $"Entry_{cat.catName}";
    
    Debug.Log($"🐱 Creating entry for: {cat.catName}, Sprite: {cat.catSprite?.name}");

    // APPROACH 1: Try to find and set Image component
    bool spriteSet = TrySetSpriteOnImage(entry, cat);
    
    // APPROACH 2: If that fails, try RawImage
    if (!spriteSet)
    {
        spriteSet = TrySetSpriteOnRawImage(entry, cat);
    }
    
    // APPROACH 3: Last resort - add a new Image component
    if (!spriteSet)
    {
        spriteSet = TryAddNewImageComponent(entry, cat);
    }

    if (spriteSet)
    {
        Debug.Log($"✅ Successfully set sprite for {cat.catName}");
    }
    else
    {
        Debug.LogError($"❌ FAILED to set sprite for {cat.catName}");
        
        // Emergency: Make the entry bright red so we can see it
        Image emergencyImage = entry.GetComponent<Image>();
        if (emergencyImage == null) emergencyImage = entry.AddComponent<Image>();
        emergencyImage.color = Color.red;
    }

    // Set cat name
    Text catNameText = entry.GetComponentInChildren<Text>();
    if (catNameText != null)
    {
        catNameText.text = cat.catName;
    }

    entry.SetActive(true);
}

private bool TrySetSpriteOnImage(GameObject entry, CatDefinition cat)
{
    Image[] allImages = entry.GetComponentsInChildren<Image>(true);
    
    foreach (Image img in allImages)
    {
        if (cat.catSprite != null)
        {
            img.sprite = cat.catSprite;
            img.color = Color.white;
            img.enabled = true;
            img.preserveAspect = true;
            
            // Force the GameObject to be active
            img.gameObject.SetActive(true);
            
            Debug.Log($"✅ Set sprite on Image: {img.gameObject.name}");
            return true;
        }
    }
    
    Debug.Log("❌ No Image components found or sprite is null");
    return false;
}

private bool TrySetSpriteOnRawImage(GameObject entry, CatDefinition cat)
{
    RawImage[] rawImages = entry.GetComponentsInChildren<RawImage>(true);
    
    foreach (RawImage rawImg in rawImages)
    {
        if (cat.catSprite != null && cat.catSprite.texture != null)
        {
            rawImg.texture = cat.catSprite.texture;
            rawImg.color = Color.white;
            rawImg.enabled = true;
            
            Debug.Log($"✅ Set texture on RawImage: {rawImg.gameObject.name}");
            return true;
        }
    }
    
    return false;
}

private bool TryAddNewImageComponent(GameObject entry, CatDefinition cat)
{
    if (cat.catSprite == null) return false;
    
    // Add a new Image component to the entry
    Image newImage = entry.AddComponent<Image>();
    newImage.sprite = cat.catSprite;
    newImage.color = Color.white;
    newImage.preserveAspect = true;
    
    // Set a reasonable size
    RectTransform rect = entry.GetComponent<RectTransform>();
    if (rect != null)
    {
        rect.sizeDelta = new Vector2(100, 100);
    }
    
    Debug.Log("✅ Added new Image component with sprite");
    return true;
}

    // Manual refresh method for testing - call this explicitly when needed
    public void ManualRefresh()
    {
        if (!isRefreshing)
        {
            RefreshCollectionDisplay();
        }
    }

    // This is called when the collection changes in any way
    private void OnCollectionChanged()
    {
        Debug.Log("🔄 Collection changed event received");
        
        // If the collection screen is currently open, refresh it
        if (gameObject.activeInHierarchy)
        {
            Invoke(nameof(RefreshCollectionDisplay), 0.1f);
        }
    }
}