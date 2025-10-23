
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
    public DrizzleType drizzle = DrizzleType.None;
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

    [Header("Toppings")] public GameObject whippedCream;
    public GameObject chocolateDrizzle;
    public GameObject caramelDrizzle;


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
        
        // start no toppings
        whippedCream.SetActive(false);
        chocolateDrizzle.SetActive(false);
        caramelDrizzle.SetActive(false);
        
        SetVisualOn(false);  // hide at first
    }
    
    
    public void PourEspresso()
    {
        Debug.Log("Calling PourEspresso from NewDrink");
        isEmpty = false;

        if (temperature == TemperatureType.Hot)
        {
            drinkSprite = hotFilledCup;
            Debug.Log("Changed sprite to hot full");
        }

        if (temperature == TemperatureType.Cold)
        {
            Debug.Log("cold drink to pour espresso");
            Debug.Log("ice cubes: " + iceCubes);
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
                Debug.Log("drink sprite icedFilled 3 ice");
            }
        }
        
        UpdateVisual();
    }
    
    

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
        //iceCubes = ice;
        if (ice > 3)
            iceCubes = 3;
        else
        {
            iceCubes = ice;
        }

        if (ice == 1)
            drinkSprite = icedEmptyCup_1ice;
        else if (ice == 2)
            drinkSprite = icedEmptyCup_2ice;
        else if (ice == 3)
            drinkSprite = icedEmptyCup_3ice;
        
        UpdateVisual();
    }
    
    public int GetIceCubes() => iceCubes;

    public void PourMilk(MilkType selectedMilk)
    {
        if (selectedMilk != MilkType.None)
        {
            if (temperature == TemperatureType.Hot)
                drinkSprite = hotFilledCupWithMilk;
            else if (temperature == TemperatureType.Cold)
            {
                if  (iceCubes == 1)
                    drinkSprite = icedFilledCupWithMilk_1ice;
                else if (iceCubes == 2)
                    drinkSprite = icedFilledCupWithMilk_2ice;
                else if (iceCubes == 3)
                    drinkSprite = icedFilledCupWithMilk_3ice;
            }
            
            UpdateVisual();
        }
        
        milk = selectedMilk;
        Debug.Log("Milk type in drink: " + milk);
    }

    public void AddTopping(ToppingsType topping)
    {
        switch (topping)
        {
            case ToppingsType.WhippedCream:
                hasWhippedCream = true;
                if (whippedCream) whippedCream.SetActive(true);
                break;
            case ToppingsType.ChocolateSyrup:
                if (whippedCream)
                {
                    drizzle = DrizzleType.Chocolate;
                    chocolateDrizzle.SetActive(true);
                }
                break;
            case ToppingsType.CaramelSyrup:
                if (whippedCream)
                {
                    drizzle = DrizzleType.Caramel;
                    caramelDrizzle.SetActive(true);
                }
                break;
        }
    }
    
    /*
    public void AddTopping(ToppingsType type)
    {
        switch (type)
        {
            case ToppingsType.WhippedCream: hasWhippedCream = true; break;
            case ToppingsType.ChocolateSyrup: syrup = SyrupType.Chocolate; break;
            case ToppingsType.CaramelSyrup: syrup = SyrupType.Caramel; break;
        }
    }
    */

    
}
