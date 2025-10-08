using UnityEngine;
using UnityEngine.UI;

public class DrinkSelector : MonoBehaviour
{
    public Button hotDrinkButton;
    public Button coldDrinkButton;
    private Drink currentDrink;

    public void SelectColdDrink()
    {
        currentDrink = new Drink(false);
        _disableButtons();
        print("Cold drink selected!");
    }

    public void SelectHotDrink()
    {
        currentDrink = new Drink(true);
        _disableButtons();
        print("Hot drink selected!");
    }

    private void _disableButtons()
    {
        hotDrinkButton.interactable = false;
        coldDrinkButton.interactable = false;
    }
}