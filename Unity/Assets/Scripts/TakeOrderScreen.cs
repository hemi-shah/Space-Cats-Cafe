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
    
    [SerializeField] private Image catPortrait;
    [SerializeField] private Text catNameText;

    // When TakeOrderScreen opens, starts sequence to show order ticket
    private void OnEnable()
    {
        if (ticketBoard)
        {
            ticketBoard.OnDetailOrderChanged += HandleDetailChanged;
        }

        HandleDetailChanged(ticketBoard?.GetCurrentDetailOrder());
        //StartCoroutine(ShowOrderTicketSequence());
    }

    private void OnDisable()
    {
        if (ticketBoard)
        {
            ticketBoard.OnDetailOrderChanged -= HandleDetailChanged;
        }
    }

    private void HandleDetailChanged(int? orderNumber)
    {
        Debug.Log($"[TakeOrderScreen] Detail changed -> {orderNumber}");
        
        if (!orderNumber.HasValue) { ClearCatUI(); return; }

        if (!CustomerManager.Instance ||
            !CustomerManager.Instance.TryGetSession(orderNumber.Value, out var session))
        {
            Debug.LogWarning($"[TakeOrderScreen] No session for order {orderNumber.Value}");
            ClearCatUI();
            return;
        }

        // Show the matching cat
        if (catPortrait) catPortrait.sprite = session.cat.catSprite;
        if (catNameText) catNameText.text = session.cat.catName;

        // Staged ticket reveal
        var ticket = ticketBoard.GetCurrentDetailTicket();
        if (ticket)
        {
            ticket.SetContentVisible(false);
            StartCoroutine(DelayedFadeIn(ticket));
        }
        // update cat panel
        /*
        if (orderNumber.HasValue &&
            CustomerManager.Instance.TryGetSession(orderNumber.Value, out var session))
        {
            if (catPortrait) catPortrait.sprite = session.cat.catSprite;
            if (catNameText) catNameText.text = session.cat.catName;

            var ticket = ticketBoard.GetCurrentDetailTicket();
            if (ticket)
            {
                ticket.SetContentVisible(false);
                StartCoroutine(DelayedFadeIn(ticket));
            }
        }
        *

        else
        {
            if (catPortrait) catPortrait.sprite = null;
            if (catNameText) catNameText.text = "";
        }
        */
        Debug.Log($"[TakeOrderScreen] order={orderNumber} " +
                  $"cat={(session != null ? session.cat.catName : "null")} " +
                  $"ticket={(ticket ? ticket.name : "null")}");
    }

    private void ClearCatUI()
    {
        if (catPortrait) catPortrait.sprite = null;
        if (catNameText) catNameText.text = "";
    }
    
    private IEnumerator DelayedFadeIn(OrderTicket ticket)
    {
        yield return new WaitForSeconds(delayBeforeContent);
        if (ticket) ticket.FadeContent(true, fadeDuration);
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
