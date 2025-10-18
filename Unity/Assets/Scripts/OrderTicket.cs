using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public struct OrderTicketData
{
    public bool isHot;
    public bool hasWhippedCream;
    public bool hasChocolateSyrup;
    public bool hasCaramelSyrup;

    public int numberOfIceCubes;
    public MilkType milk;
    public SyrupType syrup;

    public string drinkName;
}

public class OrderTicket : MonoBehaviour
{
    
    // Sprites
    public Sprite emptyCupHot;
    public Sprite emptyCupIced;

    public Sprite oneIce;
    public Sprite twoIce;
    public Sprite threeIce;

    public Sprite regularMilk;
    public Sprite oatMilk;
    public Sprite almondMilk;

    public Sprite chocolateSyrupPump;
    public Sprite caramelSyrupPump;
    public Sprite mochaSyrupPump;
    
    public Sprite whippedCream;
    public Sprite chocolateSyrup;
    public Sprite caramelSyrup;
    
    // GameObjects
    public GameObject DrinkType;
    public GameObject milkType;
    public GameObject syrupType;
    public GameObject whippedCreamTopping;
    public GameObject chocolateSyrupTopping;
    public GameObject caramelSyrupTopping;
    public GameObject iceAmount;

    [SerializeField] private Text orderNumberText;
    [SerializeField] private Text drinkNameText;

    public RectTransform contentRoot;
    
    public void SetContentScale(float s)
    {
        if (contentRoot) contentRoot.localScale = new Vector3(s, s, 1f);
    }

    // ChatGPT
    public void Setup(int orderNumber, OrderTicketData data)
    {
        if (orderNumberText)
        {
            orderNumberText.text = $"#{orderNumber}";
        }

        if (drinkNameText)
        {
            drinkNameText.text = string.IsNullOrEmpty(data.drinkName) ? "" : data.drinkName;
        }
        
        if (DrinkType)
        {
            var img = DrinkType.GetComponent<Image>();
            if (!img)
            {
                Debug.LogWarning("[OrderTicket] DrinkType is missing Image");
            }
            else
            {
                img.sprite = data.isHot ? emptyCupHot : emptyCupIced;
            }
        }
        else
        {
            Debug.LogWarning("[OrderTicket] DrinkType reference is missing");
        }

        if (whippedCreamTopping)
        {
            whippedCreamTopping.SetActive(data.hasWhippedCream);
        }

        if (chocolateSyrupTopping)
        {
            chocolateSyrupTopping.SetActive(data.hasChocolateSyrup);
        }

        if (caramelSyrupTopping)
        {
            caramelSyrupTopping.SetActive(data.hasCaramelSyrup);
        }

        if (milkType)
        {
            var img = milkType.GetComponent<Image>();
            if (img)
            {
                switch (data.milk)
                {
                    case MilkType.Dairy: img.sprite = regularMilk; break;
                    case MilkType.Almond:  img.sprite = almondMilk;  break;
                    case MilkType.Oat:     img.sprite = oatMilk;     break;
                    case MilkType.None:    img.sprite = null;        break;
                }
            }
            milkType.SetActive(data.milk != MilkType.None);
        }
        
        if (syrupType)
        {
            var img = syrupType.GetComponent<Image>();
            if (img)
            {
                switch (data.syrup)
                {
                    case SyrupType.Chocolate: img.sprite = chocolateSyrupPump; break;
                    case SyrupType.Caramel:   img.sprite = caramelSyrupPump;   break;
                    case SyrupType.Mocha:     img.sprite = mochaSyrupPump;     break;
                    case SyrupType.None:      img.sprite = null;                break;
                }
            }
            syrupType.SetActive(data.syrup != SyrupType.None);
        }
        
        // ice
        if (iceAmount)
           {
               var img = iceAmount.GetComponent<Image>();
               if (img)
               {
                   if (data.isHot || data.numberOfIceCubes <= 0) img.sprite = null;
                   else if (data.numberOfIceCubes == 1)       img.sprite = oneIce;
                   else if (data.numberOfIceCubes == 2)       img.sprite = twoIce;
                   else                                     img.sprite = threeIce; // 3+
               }
               iceAmount.SetActive(!data.isHot && data.numberOfIceCubes > 0);
           }
         
    }
    
    /*
    // ChatGpt
    [Header("UI")] 
    [SerializeField] private Text orderNumberText;
    [SerializeField] private Text drinkNameText;

    [Header("Milk Type")] 
    //[SerializeField] private GameObject milkType;
    
    [NonSerialized] public string drinkName;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
