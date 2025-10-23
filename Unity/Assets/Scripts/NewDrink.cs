
using UnityEngine;

public class NewDrink : MonoBehaviour
{

    [Header("Drink Data")] 
    public TemperatureType temperature = TemperatureType.Hot;
    [Range(0, 3)] public int iceCubes = 0;
    public MilkType milk = MilkType.None;
    public SyrupType syrup = SyrupType.None;
    public bool hasWhippedCream = false;
    public bool isServed = false;
    public Sprite drinkSprite;

    [Header("Empty Cup Visuals")] 
    public Sprite hotEmptyCup;
    public Sprite icedEmptyCup_0ice;
    public Sprite icedEmptyCup_1ice;
    public Sprite icedEmptyCup_2ice;
    public Sprite icedEmptyCup_3ice;
    
    [Header("Filled Cup Visuals")]
    public Sprite hotFilledCup;
    //public Sprite icedFilledCup_0ice; // can't have iced drink with zero ice
    public Sprite icedFilledCup_1ice;
    public Sprite icedFilledCup_2ice;
    public Sprite icedFilledCup_3ice;
    
    [Header("Filled Cup With Milk Visuals")]
    public Sprite hotFilledCupWithMilk;
    //public Sprite icedFilledCupWithMilk_0ice;
    public Sprite icedFilledCupWithMilk_1ice;
    public Sprite icedFilledCupWithMilk_2ice;
    public Sprite icedFilledCupWithMilk_3ice;

    /*
    [Header("Cup With No Milk and Whipped Cream Visuals")]
    public Sprite hotCupWithWhippedCream;
    public Sprite icedCupWithWhippedCream_0ice;
    public Sprite icedCupWithWhippedCream_1ice;
    public Sprite icedCupWithWhippedCream_2ice;
    public Sprite icedCupWithWhippedCream_3ice;
    
    [Header("Cup With Milk and Whipped Cream Visuals")]
    public Sprite hotCupWithMilkWhippedCream;
    public Sprite icedCupWithMilkWhippedCream_0ice;
    public Sprite icedCupWithMilkWhippedCream_1ice;
    public Sprite icedCupWithMilkWhippedCream_2ice;
    public Sprite icedCupWithMilkWhippedCream_3ice;
    
    [Header("Cup With Milk and Whipped Cream Visuals")]
    */
    
    //public SpriteRenderer drinkSprite; // optional, for showing the drink

    // Example: Update the sprite if needed
    public void SetSprite(Sprite sprite)
    {
        if (drinkSprite != null)
            drinkSprite = sprite;
    }

    // Simple runtime methods
    public void AddIce() => iceCubes = Mathf.Clamp(iceCubes + 1, 0, 3);
    public void RemoveIce() => iceCubes = Mathf.Max(0, iceCubes - 1);
    public void ToggleWhip() => hasWhippedCream = !hasWhippedCream;
    public void Serve() => isServed = true;



    // hot or cold
    // if cold: number of ice cubes (1 to 3)
    // milk type or none
    // syrup type
    // whipped cream topping bool
    // if whipped, choc or caramel or none
    // isEmpty
    // sprite
    // name based on contents (iced / hot, etc)

    /*
    [Header("Properties")]
    public DrinkRecipe recipe;
    public bool isServed;
    public int currentIce; // runtime may change this if player adds/steals ice
    public bool hasWhip;   // if whip can be added/removed during play
    public TemperatureType temperature;

    [Header("Visuals")]
    public Sprite runtimeSprite;
    */

    /*

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
    */
}
