
using UnityEngine;

public class NewDrink : MonoBehaviour
{
    // hot or cold
    // if cold: number of ice cubes (1 to 3)
    // milk type or none
    // syrup type
    // whipped cream topping bool
    // if whipped, choc or caramel or none
    // isEmpty
    // sprite
    // name based on contents (iced / hot, etc)
    
    public DrinkRecipe recipe;
    public bool isServed;
    public int currentIce; // runtime may change this if player adds/steals ice
    public bool hasWhip;   // if whip can be added/removed during play

    public string Name
    {
        get
        {
            if (recipe != null)
                return recipe.GetDisplayName();
            else
                return "Custom Drink";
        }
    }

    public Sprite Sprite
    {
        get
        {
            if (recipe != null && recipe.overrideSprite != null)
                return recipe.overrideSprite;
            else
                return recipe.overrideSprite;
        }
    }
}
