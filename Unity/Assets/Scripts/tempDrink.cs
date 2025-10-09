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

    void Awake()
    {
        currentImage = GetComponent<Image>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isEmpty = true;
        if (isHot)
        {
            currentImage.sprite = hotEmptySprite;
        }
        else
        {
            currentImage.sprite = icedEmptySprite;
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
    
    
}
