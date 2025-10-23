using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToppingsScreen : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject toppingsScreen;
    
    //public tempDrink drink;
    public NewDrink activeDrink;
    public DrinkManager drinkManager;
    
    [Header("Toppings")]
    public GameObject whippedCream;
    public GameObject chocolateSyrup;
    public GameObject caramelSyrup;

    [Header("Topping Location")]
    [SerializeField] private RectTransform cup;
    [SerializeField] private Vector2 offsetFromCup = new Vector2(-180f, 210f);
    [SerializeField] private float holdDelay = 0.6f;
    [SerializeField] private int maxToppings = 3;

    private bool hasWhippedCream;
    private bool hasChocolateSyrup;
    private bool hasCaramelSyrup;
    private int toppingsCount;

    private Vector2 startWhippedPos;
    private Vector2 startChocolatePos;
    private Vector2 startCaramelPos;

    private Coroutine active;

    void Start()
    {
        //startScreen.SetActive(false);
        //toppingsScreen.SetActive(true);
        activeDrink = drinkManager.GetActiveDrink();
        
        if (whippedCream) startWhippedPos = whippedCream.GetComponent<RectTransform>().anchoredPosition;
        if (chocolateSyrup) startChocolatePos = chocolateSyrup.GetComponent<RectTransform>().anchoredPosition;
        if (caramelSyrup) startCaramelPos = caramelSyrup.GetComponent<RectTransform>().anchoredPosition;
    }

    public void SelectWhippedCream()
    {
        Select(ToppingsType.WhippedCream, whippedCream);
    }

    public void SelectChocolateSyrup()
    {
        Select(ToppingsType.ChocolateSyrup, chocolateSyrup);
    }

    public void SelectCaramelSyrup()
    {
        Select(ToppingsType.CaramelSyrup, caramelSyrup);
    }
    
    public void Select(ToppingsType type, GameObject sourceGo)
    {
        /*
        if (active != null) return;
        if (activeDrink == null || cup == null || sourceGo == null) return;
        
        if (toppingsCount >= maxToppings) return;
        if (type == ToppingsType.WhippedCream && hasWhippedCream) return;
        if (type == ToppingsType.ChocolateSyrup && hasChocolateSyrup) return;
        if (type == ToppingsType.CaramelSyrup && hasCaramelSyrup) return;
        */
        
        active = StartCoroutine(TeleportThenApply(sourceGo, type));
    }
    
    private IEnumerator TeleportThenApply(GameObject sourceGo, ToppingsType type)
    {
        var rt = sourceGo.GetComponent<RectTransform>();
        if (!rt) { active = null; yield break; }

        // Normalize for predictable anchoredPosition behavior
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        // Compute target position in the topping's parent space (same pattern as your MilkScreen)
        Vector2 targetAnchored;
        var parentRT = (RectTransform)rt.parent;

        if (cup.transform.IsChildOf(parentRT))
        {
            targetAnchored = cup.anchoredPosition + offsetFromCup;
        }
        else
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRT,
                RectTransformUtility.WorldToScreenPoint(null, cup.position),
                null,
                out targetAnchored
            );
            targetAnchored += offsetFromCup;
        }

        // Remember original start pos for this specific button
        Vector2 original = GetStartPosFor(sourceGo);

        // Teleport next to the cup
        rt.anchoredPosition = targetAnchored;

        // Tilt it 65 degrees
        rt.localRotation = Quaternion.Euler(0f, 0f, -65f);
        
        // Small pause
        yield return new WaitForSeconds(holdDelay);
        
        // Reset rotation back upright
        rt.localRotation = Quaternion.identity;
        Debug.Log("Set topping back upright");

        // Apply topping to the drink (change this call if your API is different)
        // e.g. drink.AddTopping(type); or drink.ApplyTopping(type);
        activeDrink.AddTopping(type);
        Debug.Log("Adding topping to active drink in toppings screen");
        bool added = true;

        /*
        if (added)
        {
            toppingsCount++;
            if (type == ToppingsType.WhippedCream)   hasWhippedCream = true;
            if (type == ToppingsType.ChocolateSyrup) hasChocolateSyrup = true;
            if (type == ToppingsType.CaramelSyrup)   hasCaramelSyrup = true;
        }
        */

        // Snap back to original position
        rt.anchoredPosition = original;

        active = null;
    }
    
    private Vector2 GetStartPosFor(GameObject go)
    {
        if (go == whippedCream)   return startWhippedPos;
        if (go == chocolateSyrup) return startChocolatePos;
        // else caramel
        return startCaramelPos;
    }

    // Call this when starting a brand-new drink
    public void ResetToppings()
    {
        hasWhippedCream = hasChocolateSyrup = hasCaramelSyrup = false;
        toppingsCount = 0;

        // (Optional) also snap buttons back, in case they moved during a transition
        if (whippedCream)   whippedCream.GetComponent<RectTransform>().anchoredPosition   = startWhippedPos;
        if (chocolateSyrup) chocolateSyrup.GetComponent<RectTransform>().anchoredPosition = startChocolatePos;
        if (caramelSyrup)   caramelSyrup.GetComponent<RectTransform>().anchoredPosition   = startCaramelPos;
    }
    
    
}
