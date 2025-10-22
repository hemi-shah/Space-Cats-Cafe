using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    private List<Drink> drinks;
    private Drink activeDrink;

    public void AddDrink(Drink drink)
    {
        drinks.Add(drink);
    }

    public void RemoveDrink(Drink drink)
    {
        drinks.Remove(drink);
    }

    public int GetDrinkCount()
    {
        return drinks.Count;
    }

    public void SetActiveDrink(Drink drink)
    {
        activeDrink = drink;
    }

    public Drink GetActiveDrink()
    {
        return activeDrink;
    }
}
