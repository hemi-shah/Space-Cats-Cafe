using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    [Header("Prefab Reference")]
    public GameObject newDrinkPrefab;

    private List<NewDrink> drinks = new List<NewDrink>();
    private NewDrink activeDrink;

    public Canvas canvas;

    public NewDrink CreateDrink(TemperatureType temperature, int iceCubes = 0, Vector3? spawnPosition = null)
    {
        if (newDrinkPrefab == null)
        {
            Debug.LogError("NewDrink prefab not assigned in DrinkManager!");
            return null;
        }

        //Vector3 pos = spawnPosition ?? Vector3.zero;
        //GameObject drinkObj = Instantiate(newDrinkPrefab, pos, Quaternion.identity);
        
        GameObject drink = Instantiate(newDrinkPrefab);
        
        if (canvas != null)
            drink.transform.SetParent(canvas.transform, false);
        
        NewDrink drinkComp = drink.GetComponent<NewDrink>();

        if (drinkComp != null)
        {
            drinkComp.temperature = temperature;
            drinkComp.iceCubes = iceCubes;

            if (spawnPosition.HasValue)
            {
                RectTransform rt = drink.GetComponent<RectTransform>();
                if (rt != null)
                    rt.anchoredPosition = (Vector2)spawnPosition.Value;
            }
        }

        drinks.Add(drinkComp);
        activeDrink = drinkComp;

        return drinkComp;
    }

    public void RemoveDrink(NewDrink drink)
    {
        if (drinks.Contains(drink))
        {
            drinks.Remove(drink);
            Destroy(drink.gameObject);
        }
    }

    public List<NewDrink> GetAllDrinks() => drinks;

    public void SetActiveDrink(NewDrink drink) => activeDrink = drink;

    public NewDrink GetActiveDrink() => activeDrink;
}
