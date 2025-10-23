
using UnityEngine;
using UnityEngine.UI;

public class NewDrink : MonoBehaviour
{

    [Header("Drink Data")] 
    public TemperatureType temperature = TemperatureType.Hot;
    public bool isEmpty = true;
    [Range(0, 3)] public int iceCubes = 0;
    public MilkType milk = MilkType.None;
    public SyrupType syrup = SyrupType.None;
    public bool hasWhippedCream = false;
    public bool isServed = false;
    public Sprite drinkSprite;
    public Image drinkImage;

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


    public void UpdateVisual()
    {
        if (drinkSprite == null)
        {
            Debug.LogError("Drink Sprite is null");
        }

        if (drinkImage != null)
        {
            drinkImage.sprite = drinkSprite;
        }
    }

    public void SetVisualOn(bool isOn)
    {
        if (drinkImage == null)
        {
            Debug.LogError("Drink Image is null");
            return;
        }
        
        if (isOn)
            drinkImage.enabled = true;
        else
            drinkImage.enabled = false;
    }
    
    public void SetInitialSprite(TemperatureType temp, int ice)
    {
        if (temp.Equals(TemperatureType.Hot))
        {
            drinkSprite = hotEmptyCup;
        }

        if (temp.Equals(TemperatureType.Cold))
        {
            if (ice == 0)
                drinkSprite = icedEmptyCup_0ice;
            else if (ice == 1)
                drinkSprite = icedEmptyCup_1ice;
            else if (ice == 2)
                drinkSprite = icedEmptyCup_2ice;
            else if (ice == 3)
                drinkSprite = icedEmptyCup_3ice;
        }
        
        SetVisualOn(false);  // hide at first
    }
    
    
    public void PourEspresso()
    {
        isEmpty = false;

        if (temperature == TemperatureType.Hot)
        {
            drinkSprite = hotFilledCup;
        }

        if (temperature == TemperatureType.Cold)
        {
            if (iceCubes == 1)
            {
                drinkSprite = icedFilledCup_1ice;
            }
            else if (iceCubes == 2) 
            {
                drinkSprite = icedFilledCup_2ice;
            }
            else if (iceCubes == 3)
            {
                drinkSprite = icedFilledCup_3ice;
            }
        }
        
        UpdateVisual();
    }
    

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
    //public void AddIce() => iceCubes = Mathf.Clamp(iceCubes + 1, 0, 3);
    public void RemoveIce() => iceCubes = Mathf.Max(0, iceCubes - 1);
    public void ToggleWhip() => hasWhippedCream = !hasWhippedCream;
    public void Serve() => isServed = true;

    // set number of ice cubes after minigame
    public void SetIceCubes(int ice)
    {
        iceCubes = ice;

        if (ice == 1)
            drinkSprite = icedEmptyCup_1ice;
        else if (ice == 2)
            drinkSprite = icedEmptyCup_2ice;
        else if (ice == 3)
            drinkSprite = icedEmptyCup_3ice;
        
        UpdateVisual();
    }
    
    public int GetIceCubes() => iceCubes;

    
}
