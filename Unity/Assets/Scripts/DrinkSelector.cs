using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrinkSelector : MonoBehaviour
{
    public Button hotDrinkButton;
    public Button coldDrinkButton;

    public CupAnimator hotCupAnimator;
    public CupAnimator coldCupAnimator;

    private NewDrink currentDrink;
    private bool selected = false;
    
    public DrinkManager drinkManager;
    public ScreenManager screenManager;

    public float delayBeforeScreenChange = 1.5f;

    private void Start()
    {
        if (drinkManager == null)
        {
            Debug.LogWarning("Drink selector has no drink manager");
        }

        
        if (screenManager == null)
        {
            screenManager = FindObjectOfType<ScreenManager>();
            if (screenManager == null)
                Debug.LogWarning("Drink selector has no screen manager");
        }
    }

    public void SelectColdDrink()
    {
        if (selected) return;
        selected = true;

        //currentDrink = new Drink(false);
        if (drinkManager == null)
        {
            Debug.LogError("DrinkManager not set");
        }
        
        currentDrink = drinkManager.CreateDrink(TemperatureType.Cold);
        Debug.Log("Cold drink selected!");
        _disableButtons();

        coldCupAnimator.SlideToCenter();
        hotCupAnimator.SlideOutLeft();
    }

    public void SelectHotDrink()
    {
        if (selected) return;
        selected = true;

        //currentDrink = new Drink(true);
        currentDrink = drinkManager.CreateDrink(TemperatureType.Hot);
        Debug.Log("Hot drink selected!");
        _disableButtons();

        hotCupAnimator.SlideToCenter();
        coldCupAnimator.SlideOutRight();

        StartCoroutine(SwitchToSyrupScreen());
    }

    private void _disableButtons()
    {
        hotDrinkButton.interactable = false;
        coldDrinkButton.interactable = false;
    }

    private IEnumerator SwitchToSyrupScreen()
    {
        yield return new WaitForSeconds(delayBeforeScreenChange);
        screenManager.NavigateTo("SyrupSelectionScreen");
    }
}