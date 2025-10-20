using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// all ChatGPT again

public class TakeOrderScreen : MonoBehaviour
{

    [Header("Refs")]
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private TicketBoard ticketBoard;
    
    [Header("Timing")]
    [SerializeField] private float delayBeforeContent = 0.45f;
    [SerializeField] private bool useFade = true;
    [SerializeField] private float fadeDuration = 0.25f;

    // When TakeOrderScreen opens, starts sequence to show order ticket
    private void OnEnable()
    {
        StartCoroutine(ShowOrderTicketSequence());
    }

    private IEnumerator ShowOrderTicketSequence()
    {
        if (!orderManager)
        {
            Debug.LogWarning("[TakeOrderScreen] OrderManager is null!");
            yield break;
        }

        // generate an order (this is hardcoded)
        /*
        OrderTicketData data = new OrderTicketData
        {
            isHot = true,
            hasWhippedCream = true,
            hasChocolateSyrup = true,
            hasCaramelSyrup = false,
            numberOfIceCubes = 0,
            milk = MilkType.Dairy,
            syrup = SyrupType.Mocha,
            drinkName = "Hot Mocha Latte"
        };
        */

        // generate a random order
        OrderTicketData data = orderManager.GenerateRandomOrderData();

        int orderNumber = orderManager.CreateOrder(data);
        
        // Get ticket generated and hide content
        var t = ticketBoard.GetCurrentDetailTicket();
        if (t != null)
        {
            t.SetContentVisible(false);
        }
        
        // Wait
        yield return new WaitForSeconds(delayBeforeContent);

        // Fade to show content
        if (t != null)
        {
            t.FadeContent(true, fadeDuration);
        }
        
    }

}
