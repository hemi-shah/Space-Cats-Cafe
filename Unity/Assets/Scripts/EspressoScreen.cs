using UnityEngine;

public class EspressoScreen : MonoBehaviour
{
    public DrinkManager drinkManager;
    public NewDrink activeDrink;
    public GameObject startButton;

    private void OnEnable()
    {
        if (drinkManager != null)
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
    
    
}
