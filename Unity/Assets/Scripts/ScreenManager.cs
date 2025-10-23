using UnityEngine;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour
{
    [System.Serializable]
    public class GameScreen
    {
        public string screenId;
        public GameObject screenObject;
        public bool isStation = false; // Stations remain active in background
    }
    
    public DrinkManager drinkManager;
    private NewDrink activeDrink;

    [Header("Screen References")]
    [SerializeField] private List<GameScreen> screens = new List<GameScreen>();

    [Header("Ticket Board Pinning")]
    [SerializeField] private GameObject ticketBoardRoot;
    [SerializeField] private List<string> suppressBoardOnScreens = new List<string>
    {
        "StartScreen",
        "ChooseCatScreen",
        "OrderPageScreen",
        "CollectedCatsScreen",
    };
    [SerializeField] private bool hideBoardInsteadOfPushBack = false;

    [Header("Active Drink Visibility")]
    [SerializeField] private List<string> suppressActiveDrink = new List<string>()
    {
        "StartScreen",
        "ChooseCatScreen",
        "OrderPageScreen",
        "TakeOrderScreen"
    };
    
    [Header("Menu Bar")]
    [SerializeField] private GameObject menuBar; // Add this line
    
    [Header("Station Screens")]
    [SerializeField] private string ordersStation = "OrderPageScreen";
    [SerializeField] private string customizationsStation = "CustomizationsScreen"; 
    [SerializeField] private string assembleStation = "AssembleScreen";

    private Dictionary<string, GameScreen> screenDictionary;
    private string currentScreenId;

    private void Awake()
    {
        screenDictionary = new Dictionary<string, GameScreen>();
    
        // First, deactivate ALL screens
        foreach (var screen in screens)
        {
            screenDictionary[screen.screenId] = screen;
            screen.screenObject.SetActive(false); // Make sure this line exists!
        }
    
        // Then activate only the StartScreen
        ShowScreen("StartScreen"); // Changed back to StartScreen

        //ShowScreen("OrderPageScreen");  // Commented out testing line
    }

    // For main navigation flow (Start → ChooseCat → OrderPage, etc.)
    public void NavigateTo(string screenId)
    {
        Debug.Log($"NavigateTo called with: {screenId}");
        Debug.Log($"Current screen: {currentScreenId}");
    
        if (!screenDictionary.ContainsKey(screenId)) 
        {
            Debug.LogError($"Screen '{screenId}' not found in dictionary!");
            Debug.Log($"Available screens: {string.Join(", ", screenDictionary.Keys)}");
            return;
        }
        if (!screenDictionary.ContainsKey(screenId)) return;
        
        // Hide current screen
        if (!string.IsNullOrEmpty(currentScreenId))
        {
            var currentScreen = screenDictionary[currentScreenId];
            
            // Only deactivate if it's not a station
            if (!currentScreen.isStation)
            {
                currentScreen.screenObject.SetActive(false);
            }
            else
            {
                // Station screens just get moved to background but stay active
                MoveToBackground(currentScreen);
            }
        }
        
        ShowScreen(screenId);
    }

    // For quick station switching via menu bar
    public void SwitchToStation(string stationId)
    {
        if (!screenDictionary.ContainsKey(stationId)) return;
        
        // Hide current screen but keep stations active
        if (!string.IsNullOrEmpty(currentScreenId))
        {
            var currentScreen = screenDictionary[currentScreenId];
            if (!currentScreen.isStation)
            {
                currentScreen.screenObject.SetActive(false);
            }
            else
            {
                MoveToBackground(currentScreen);
            }
        }
        
        ShowScreen(stationId);
    }

    private void ShowScreen(string screenId)
    {
        activeDrink = drinkManager.GetActiveDrink();
        var screen = screenDictionary[screenId];
        screen.screenObject.SetActive(true);
        BringToForeground(screen);
        currentScreenId = screenId;
        UpdateTicketBoardLayer(screenId); // keep ticketboard on top
        
        // Add this line to always show menu bar on game screens
        UpdateMenuBarVisibility(screenId);

        if (activeDrink != null)
        {
            BringActiveDrinkToFront(screenId);
        }
        
        Debug.Log($"Showing: {screenId}");
    }

    private void MoveToBackground(GameScreen screen)
    {
        // You could add fade effects or move position here
        // For now, just ensure it's active but not the focused screen
        screen.screenObject.transform.SetAsFirstSibling(); // Put behind other UI
    }

    private void BringToForeground(GameScreen screen)
    {
        // Bring to front of UI hierarchy
        screen.screenObject.transform.SetAsLastSibling();
    }

    private void UpdateTicketBoardLayer(string screenId)
    {
        if (!ticketBoardRoot) return;
        bool suppress = suppressBoardOnScreens.Contains(screenId);

        if (suppress)
        {
            if (hideBoardInsteadOfPushBack)
            {
                ticketBoardRoot.SetActive(false);
            }
            else
            {
                if (!ticketBoardRoot.activeSelf) ticketBoardRoot.SetActive(true);
                ticketBoardRoot.transform.SetAsFirstSibling();
            }
        }

        else
        {
            // bring ticket board to top
            if (!ticketBoardRoot.activeSelf) ticketBoardRoot.SetActive(true);
            ticketBoardRoot.transform.SetAsLastSibling();
        }
    }

    private void BringActiveDrinkToFront(string screenId)
    {
        if (activeDrink == null) return;
        
        bool suppress = suppressActiveDrink.Contains(screenId);
        
        if (suppress)
        {
            activeDrink.transform.SetAsFirstSibling();
        }

        else
        {
            activeDrink.transform.SetAsLastSibling();
        }
    }

    // Add this method to handle menu bar visibility
    private void UpdateMenuBarVisibility(string screenId)
    {
        if (menuBar == null) return;
        
        // Always show menu bar on ALL screens after game starts
        // Only hide on StartScreen, show on everything else
        bool showMenuBar = screenId != "StartScreen";
        
        menuBar.SetActive(showMenuBar);
        if (showMenuBar)
        {
            menuBar.transform.SetAsLastSibling();
        }
        
        Debug.Log($"Menu bar on {screenId}: {showMenuBar}");
    }

    // ADD THIS METHOD: Manually show the menu bar
    public void ShowMenuBar()
    {
        if (menuBar != null)
        {
            menuBar.SetActive(true);
            menuBar.transform.SetAsLastSibling();
            Debug.Log("Menu bar shown manually via ShowMenuBar()");
        }
    }

    // Quick access methods for your specific stations
    public void GoToOrdersStation() => SwitchToStation(ordersStation);
    public void GoToCustomizationsStation() => SwitchToStation(customizationsStation);
    public void GoToAssembleStation() => SwitchToStation(assembleStation);

    public string GetCurrentScreen() => currentScreenId;
}