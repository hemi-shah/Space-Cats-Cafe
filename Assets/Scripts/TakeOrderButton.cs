using UnityEngine;

public class TakeOrderButton : MonoBehaviour
{

    [SerializeField] private CatDefinition cat;
    [SerializeField] private CustomerManager customerManager;
    [SerializeField] private ScreenManager screenManager;

    public void TakeOrder()
    {
        if (!cat || !customerManager || !screenManager)
        {
            Debug.LogError("[TakeOrderButton] missing references", this);
            return;
        }

        int orderNum = customerManager.TakeOrderForCat(cat);
        Debug.Log($"[TakeOrderButton] Started order {orderNum} for cat {cat.catName}", this);
        //customerManager.TakeOrderForCat(cat);   // create random order and ticket
        screenManager.NavigateTo("TakeOrderScreen");   // go to take order screen
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
