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
    }

    private void _disableButtons()
    {
        hotDrinkButton.interactable = false;
        coldDrinkButton.interactable = false;
    }
}