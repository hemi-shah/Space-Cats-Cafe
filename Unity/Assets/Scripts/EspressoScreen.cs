using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EspressoScreen : MonoBehaviour
{
    public DrinkManager drinkManager;
    public NewDrink activeDrink;
    public GameObject startButton;

    [Header("Filling Settings")] 
    public float fillDelay = 1f;

    private void OnEnable()
    {
        if (drinkManager == null)
        {
            Debug.LogError("No drink manager");
        }
        
        activeDrink = drinkManager.GetActiveDrink();

        if (activeDrink == null)
        {
            Debug.LogError("No active drink");
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
    }
    
    
}
