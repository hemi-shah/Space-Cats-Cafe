using UnityEngine;
using UnityEngine.UI;

public class tempDrink : MonoBehaviour
{
    private bool isHot;
    private bool isEmpty;
    private Image currentImage;
    
    public Sprite hotEmptySprite;
    public Sprite icedEmptySprite;
    
    public Sprite hotFullSprite;
    public Sprite icedFullSprite;

    public Sprite hotMilkCoffeeSprite;
    public Sprite icedMilkCoffeeSprite;

    public GameObject whippedCreamVisual;
    public GameObject chocolateVisual;
    public GameObject caramelVisual;

    private MilkType milkType = MilkType.None;

    private bool hasWhippedCream;
    private bool hasChocolateSyrup;
    private bool hasCaramelSyrup;
    private int toppingsCount;
    private int maxToppings = 3;
    
    private ILogger logger = new DebugLogger();

    void Awake()
    {
        currentImage = GetComponent<Image>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set empty sprites
        if (isHot && isEmpty)
        {
            currentImage.sprite = hotEmptySprite;
        }
        else if (!isHot && isEmpty)
        {
            currentImage.sprite = icedEmptySprite;
        }
        
        // set full (no milk) sprites
        else if (isHot && !isEmpty && (milkType == MilkType.None))
        {
            currentImage.sprite = hotFullSprite;
        }
        else if (isHot && !isEmpty && (milkType == MilkType.None))
        {
            currentImage.sprite = icedFullSprite;
        }
        
        // set full (with milk) sprites
        else if (isHot && !isEmpty && (milkType != MilkType.None))
        {
            currentImage.sprite = hotMilkCoffeeSprite;
        }
        else if (!isHot && !isEmpty && (milkType != MilkType.None))
        {
            currentImage.sprite = icedMilkCoffeeSprite;
        }

        if (!hasWhippedCream)
        {
            whippedCreamVisual.SetActive(false);
        }

        if (!hasChocolateSyrup)
        {
            chocolateVisual.SetActive(false);
        }

        if (!hasCaramelSyrup)
        {
            caramelVisual.SetActive(false);
        }
    }

    public bool GetIsHot()
    {
        return isHot;
    }

    public void SetIsHot(bool isHot)
    {
        this.isHot = isHot;
        if (isEmpty)
        {
            currentImage.sprite = hotEmptySprite;
        }
        else
        {
            currentImage.sprite = hotFullSprite;
        }
        
    }

    public bool GetIsEmpty()
    {
        return isEmpty;
    }

    public void SetIsEmpty(bool isEmpty)
    {
        this.isEmpty = isEmpty;
    }

    public void FillCup()
    {
        if (isHot)
        {
            currentImage.sprite = hotFullSprite;
        }
        else
        {
            currentImage.sprite = icedFullSprite;
        }
        
        isEmpty = false;
    }

    public void PourMilk(MilkType milkType)
    {
        if (isHot)
        {
            currentImage.sprite = hotMilkCoffeeSprite;
        }
        else
        {
            currentImage.sprite = icedMilkCoffeeSprite;
        }
        
        this.milkType = milkType;
    }

    public void AddTopping(ToppingsType toppingsType)
    {
        switch (toppingsType)
        {
            case ToppingsType.WhippedCream:
                if (hasWhippedCream) return;
                hasWhippedCream = true;
                if (whippedCreamVisual) whippedCreamVisual.SetActive(true);
                break;
            
            case ToppingsType.ChocolateSyrup:
                if (hasChocolateSyrup) return;
                hasChocolateSyrup = true;
                if (chocolateVisual) chocolateVisual.SetActive(true);
                break;
            
            case ToppingsType.CaramelSyrup:
                if (hasCaramelSyrup) return;
                hasCaramelSyrup = true;
                if (caramelVisual) caramelVisual.SetActive(true);
                break;
        }

        toppingsCount++;
        //logger.Log($"Added topping: {toppingsType}");
    }
    
    
}
