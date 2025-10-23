using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class DrinkManager : MonoBehaviour
{
    private List<NewDrink> drinks;
    private NewDrink activeDrink;

    public event Action<NewDrink> OnActiveDrinkChanged;

    private void Awake()
    {
        if (drinks == null)
        {
            drinks = new List<NewDrink>();
        }
    }

    public NewDrink CreateDrink(TemperatureType temperature, int startingIce)
    {
        NewDrink newDrink = new NewDrink(temperature, startingIce);
        AddDrink(newDrink);
        SetActiveDrink(newDrink);
        return newDrink;
    }
    
    public NewDrink CreateDrinkFromRecipe(DrinkRecipe recipe)
    {
        NewDrink newDrink = new NewDrink(recipe);
        drinks.Add(newDrink);
        SetActiveDrink(newDrink);
        return newDrink;
    }
    
    public void AddDrink(NewDrink drink)
    {
        drinks.Add(drink);
    }

    public void RemoveDrink(NewDrink drink)
    {
        drinks.Remove(drink);
    }

    public int GetDrinkCount()
    {
        return drinks.Count;
    }

    public void SetActiveDrink(NewDrink drink)
    {
        activeDrink = drink;
    }

    public NewDrink GetActiveDrink()
    {
        return activeDrink;
    }
}
