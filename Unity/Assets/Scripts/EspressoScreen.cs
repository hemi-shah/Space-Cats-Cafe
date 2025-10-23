using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EspressoScreen : MonoBehaviour
{
    public DrinkManager drinkManager;
    public NewDrink activeDrink;
    public GameObject startButton;
    public GameObject MilkButton;

    [Header("Filling Settings")] 
    public float fillDelay = 1f;
    
    private ILogger logger = new DebugLogger();

    private void OnEnable()
    {
        MilkButton.SetActive(false);
        if (drinkManager == null)
        {
            logger.LogError("No drink manager");
        }
        
        activeDrink = drinkManager.GetActiveDrink();
        activeDrink.SetVisualOn(true);
        logger.Log("set drink visual on");

        if (activeDrink == null)
        {
            logger.LogError("No active drink");
            return;
        }
    }

    public void PourEspresso()
    {
        if (activeDrink != null)
        {
            StartCoroutine(FillCupRoutine());
        }
    }

    private IEnumerator FillCupRoutine()
    {
        yield return new WaitForSeconds(fillDelay);
        
        activeDrink.PourEspresso();
        yield return new WaitForSeconds(1f);
        MilkButton.SetActive(true);
    }
    
    
}
