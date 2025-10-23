using UnityEngine;
using UnityEngine.UI;

public class CustomerCat : MonoBehaviour
{
    [Header("UI")] [SerializeField] private Image portrait;
    [SerializeField] private Text nameText;
    [SerializeField] private Button takeOrderButton;

    private CatDefinition cat;
    private SeatingManager seating;
    private int seatIndex = -1;

    private CustomerManager customerManager;
    private ScreenManager screenManager;

    private ILogger logger = new DebugLogger();

    public void Init(SeatingManager seatingManager, int seatIdx, CatDefinition catDef,
        CustomerManager cm, ScreenManager sm)
    {
        seating = seatingManager;
        seatIndex = seatIdx;
        cat = catDef;
        customerManager = cm;
        screenManager = sm;

        if (portrait) portrait.sprite = cat ? cat.catSprite : null;
        if (nameText) nameText.text = cat ? cat.catName : "";

        if (takeOrderButton)
        {
            takeOrderButton.onClick.RemoveAllListeners();
            takeOrderButton.onClick.AddListener(OnTakeOrderClicked);
        }
    }

    private void OnTakeOrderClicked()
    {
        if (!cat || !customerManager || !screenManager || seating == null || seatIndex < 0) 
            return;

        // ADD CAT TO COLLECTION HERE - Before taking the order
        AddCatToCollection(cat);

        int orderNum = customerManager.TakeOrderForCat(cat);

        gameObject.SetActive(false);

        seating.OnSeatTaken(seatIndex, orderNum);

        screenManager.NavigateTo("TakeOrderScreen");
    }

    // NEW: Directly add cat to collection when order is taken
    private void AddCatToCollection(CatDefinition catToAdd)
    {
        if (catToAdd == null)
        {
            logger.LogError("Cannot add null cat to collection!");
            return;
        }

        // Try to find existing CatCollectionManager
        CatCollectionManager collectionManager = FindFirstObjectByType<CatCollectionManager>();
        
        // If not found, create one
        if (collectionManager == null)
        {
            logger.Log("Creating new CatCollectionManager...");
            GameObject collectionObj = new GameObject("CatCollectionManager");
            collectionManager = collectionObj.AddComponent<CatCollectionManager>();
            DontDestroyOnLoad(collectionObj);
        }

        // Add the cat to collection
        collectionManager.AddCatToCollection(catToAdd);
        logger.Log($"✅ Added {catToAdd.catName} to cat collection! Total collected: {collectionManager.GetCollectionCount()}");
    }

    // Add this method to check if this customer is using the player's cat
    public bool IsUsingPlayerCat()
    {
        if (cat == null) return false;
        
        // Check if this cat matches the player's selected cat
        CatSelectionManager catManager = FindFirstObjectByType<CatSelectionManager>();
        if (catManager != null)
        {
            return cat.catName == catManager.GetCurrentCatType();
        }
        
        return false;
    }
}