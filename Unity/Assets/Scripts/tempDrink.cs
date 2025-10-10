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

    private MilkType milkType = MilkType.None;

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
    
    
}
