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

    [Header("Screen References")]
    [SerializeField] private List<GameScreen> screens = new List<GameScreen>();
    
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
        ShowScreen("StartScreen"); // Make sure this line exists!
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
        var screen = screenDictionary[screenId];
        screen.screenObject.SetActive(true);
        BringToForeground(screen);
        currentScreenId = screenId;
        
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

    // Quick access methods for your specific stations
    public void GoToOrdersStation() => SwitchToStation(ordersStation);
    public void GoToCustomizationsStation() => SwitchToStation(customizationsStation);
    public void GoToAssembleStation() => SwitchToStation(assembleStation);

    public string GetCurrentScreen() => currentScreenId;
}