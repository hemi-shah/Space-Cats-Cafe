using UnityEngine;

public class SendDrinkButton : MonoBehaviour
{
    public DrinkManager drinkManager;

    public void SendDrinkToNextStation()
    {
        NewDrink activeDrink = drinkManager.GetActiveDrink();
        if (activeDrink != null)
        {
            activeDrink.SendToNextStation();
            
        }
    }
}
