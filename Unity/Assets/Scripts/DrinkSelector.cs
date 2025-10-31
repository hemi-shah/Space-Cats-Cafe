using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkSelector : MonoBehaviour
{
    public Button hotDrinkButton;
    public Button coldDrinkButton;

    public CupAnimator hotCupAnimator;
    public CupAnimator coldCupAnimator;

    private NewDrink currentDrink;
    private bool selected = false;
    
    private bool selectedHot = true;
    
    public DrinkManager drinkManager;
    public GameObject sendToNextStationButton;
    
    private ILogger logger = new DebugLogger();

    void Awake()
    {
        sendToNextStationButton.SetActive(false);
    }

    public void SelectColdDrink()
    {
        if (selected) return;
        selected = true;
        selectedHot = false;

        //currentDrink = new Drink(false);
        if (drinkManager == null)
        {
            logger.LogError("DrinkManager not set");
        }
        
        currentDrink = drinkManager.CreateDrink(TemperatureType.Cold);
        
        currentDrink.SetVisualOn(false);
        
        logger.Log("Cold drink selected!");
        _disableButtons();

        coldCupAnimator.SlideToCenter();
        hotCupAnimator.SlideOutLeft();

        //StartCoroutine(ActivateButtonRoutine(0.5f));
    }

    public void SelectHotDrink()
    {
        if (selected) return;
        selected = true;
        selectedHot = true;

        //currentDrink = new Drink(true);
        currentDrink = drinkManager.CreateDrink(TemperatureType.Hot);
        logger.Log("Hot drink selected!");
        _disableButtons();

        hotCupAnimator.SlideToCenter();
        coldCupAnimator.SlideOutRight();

        StartCoroutine(ActivateButtonRoutine(0.5f));
    }

    public void SendDrink()
    {
        logger.Log("Sending drink...");
        if (selectedHot)
        {
            hotCupAnimator.SlideOutRight();
            coldCupAnimator.SlideOutRight();
        }

        else
        {
            coldCupAnimator.SlideOutRight();
            hotCupAnimator.SlideOutRight();
        }

        if (drinkManager == null)
        {
            logger.LogError("DrinkManager not set");
            return;
        }
        
        NewDrink activeDrink = drinkManager.GetActiveDrink();
        if (activeDrink != null)
        {
            activeDrink.SendToStation(DrinkStation.Espresso);
        }
        else
        {
            logger.LogError("No active drink");
        }
    }

    private void _disableButtons()
    {
        hotDrinkButton.interactable = false;
        coldDrinkButton.interactable = false;
    }

    private IEnumerator ActivateButtonRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        sendToNextStationButton.SetActive(true);
    }
    
    /*
    private IEnumerator sendDrinkToNextStation(float delay)
    {
        yield return new WaitForSeconds(delay);
        
    }
    */
    
}