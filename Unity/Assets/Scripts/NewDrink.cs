
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

    public Sprite runtimeSprite;
    public TemperatureType temperature;

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

    // constructor to start with a temperature and number of ice
    public NewDrink(TemperatureType temperature, int startingIce)
    {
        this.recipe = null;
        this.temperature = temperature;
        this.isServed = false;
        this.hasWhip = false;
        this.runtimeSprite = null;

        if (temperature == TemperatureType.Cold)
        {
            this.currentIce = Mathf.Clamp(startingIce, 0, 3);
        }
        else
        {
            this.currentIce = 0;
        }
    }
    
    public NewDrink(DrinkRecipe recipe)
    {
        this.recipe = recipe;
        this.isServed = false;

        if (recipe != null)
        {
            if (recipe.temperature == TemperatureType.Cold)
            {
                this.currentIce = Mathf.Clamp(recipe.iceCubes, 0, 3);
            }
            else
            {
                this.currentIce = 0;
            }

            this.hasWhip = recipe.hasWhippedCream;
            this.runtimeSprite = recipe.overrideSprite;
        }
        else
        {
            this.currentIce = 0;
            this.hasWhip = false;
            this.runtimeSprite = null;
        }
    }
    
    // Runtime mutator methods
    public void AddIce()
    {
        if (recipe != null || recipe.temperature == TemperatureType.Cold)
        {
            currentIce = Mathf.Clamp(currentIce + 1, 0, 3);
        }
    }

    public void RemoveIce()
    {
        currentIce = Mathf.Max(0, currentIce - 1);
    }

    public void ToggleWhip()
    {
        hasWhip = !hasWhip;
    }

    public void Serve()
    {
        isServed = true;
    }

    public bool IsIced()
    {
        if (recipe == null)
        {
            return currentIce > 0;
        }
        return recipe.temperature == TemperatureType.Cold;
    }
}
