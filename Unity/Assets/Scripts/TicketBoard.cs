using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// basically all ChatGPT bc like idk this already took like hours

public class TicketBoard : MonoBehaviour
{
    public event Action<int?> OnDetailOrderChanged;
    
    [Header("Prefabs & Parents")]
    [SerializeField] private OrderTicket ticketPrefab;
    [SerializeField] private RectTransform topRowParent;
    [SerializeField] private RectTransform detailParent;
    
    [Header("Exact Sizes")]
    [SerializeField] private Vector2 largeSize = new Vector2(500f, 700f);
    [SerializeField] private Vector2 smallSize = new Vector2(150f, 250f);

    [Header("Content Scales")]
    [SerializeField] private float largeContentScale = 1f;
    [SerializeField] private float smallContentScale = 0.4f;

    private readonly Dictionary<int, OrderTicket> tickets = new Dictionary<int, OrderTicket>();
    private int? currentDetailOrder = null;
    
    public int? GetCurrentDetailOrder() => currentDetailOrder;
    
    public void SpawnTicket(int orderNumber, OrderTicketData data)
    {
        if (!ticketPrefab || !topRowParent || !detailParent)
        {
            Debug.LogError("[TicketBoard] Assign ticketPrefab, topRowParent, detailParent");
            return;
        }

        if (currentDetailOrder.HasValue &&
            tickets.TryGetValue(currentDetailOrder.Value, out var currentBig) &&
            currentBig != null)
        {
            MoveToTopRow(currentBig);
        }
        
        // First ticket appears big, others go to top row
        //bool spawnInDetail = currentDetailOrder == null;
        //RectTransform parent = spawnInDetail ? detailParent : topRowParent;

        // Create ticket
        var ticket = Instantiate(ticketPrefab, detailParent);
        var rt = (RectTransform)ticket.transform;
        Normalize(rt);
        
        ticket.Setup(orderNumber, data);
        tickets[orderNumber] = ticket;

        // Wire up button click (no IPointerClickHandler needed)
        var btn = ticket.GetComponent<Button>();
        if (btn)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnTicketClicked(orderNumber));
        }

        CenterInParent(rt);
        SetTicketSize(rt, largeSize);
        ticket.SetContentScale(largeContentScale);
        ticket.transform.SetAsLastSibling();
        
        //currentDetailOrder = orderNumber;
        SetDetailOrder(orderNumber);

    }
    
    public void RemoveTicket(int orderNumber)
    {
        if (tickets.TryGetValue(orderNumber, out var t) && t)
        {
            if (currentDetailOrder == orderNumber) currentDetailOrder = null;
            Destroy(t.gameObject);
            tickets.Remove(orderNumber);
        }
    }
    
    // When ticket clicked, moves between detail area and top row
    public void OnTicketClicked(int orderNumber)
    {
        if (!tickets.TryGetValue(orderNumber, out var clicked) || clicked == null)
            return;

        bool clickedIsDetail = clicked.transform.parent == detailParent;

        if (clickedIsDetail)
        {
            // Move big ticket back up top (small)
            MoveToTopRow(clicked);
            currentDetailOrder = null;
        }
        else
        {
            // Move current detail ticket back up
            if (currentDetailOrder.HasValue &&
                tickets.TryGetValue(currentDetailOrder.Value, out var currentBig) &&
                currentBig != null)
            {
                MoveToTopRow(currentBig);
            }

            // Move clicked small one to detail (big)
            MoveToDetail(clicked);
            currentDetailOrder = orderNumber;
        }
        
        // debug to see who owns ticket
        if (CustomerManager.Instance &&
            CustomerManager.Instance.TryGetSession(orderNumber, out var session))
        {
            Debug.Log($"[TicketBoard] Clicked ticket #{orderNumber} belongs to cat '{session.cat.catName}");
        }
    }

    // move ticket to top row
    private void MoveToTopRow(OrderTicket t)
    {
        t.transform.SetParent(topRowParent, false);
        var rt = (RectTransform)t.transform;
        SetTicketSize(rt, smallSize);
        t.SetContentScale(smallContentScale);
        t.transform.SetAsLastSibling();
    }
    
    // move ticket to detail area
    private void MoveToDetail(OrderTicket t)
    {
        t.transform.SetParent(detailParent, false);
        var rt = (RectTransform)t.transform;
        CenterInParent(rt);
        SetTicketSize(rt, largeSize);
        t.SetContentScale(largeContentScale);
    }
    
    private void CenterInParent(RectTransform rt)
    {
        if (!rt) return;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
    }

    private void Normalize(RectTransform rt)
    {
        if (!rt) return;
        rt.localScale = Vector3.one;
    }

    private void SetTicketSize(RectTransform rt, Vector2 size)
    {
        var le = rt.GetComponent<LayoutElement>();
        if (le)
        {
            le.preferredWidth = le.minWidth = size.x;
            le.preferredHeight = le.minHeight = size.y;
            le.flexibleWidth = le.flexibleHeight = 0f;
        }
        else
        {
            rt.sizeDelta = size;
        }
        
        //Debug.Log($"SetTicketSize called for {rt.name}: target size = {size.x} x {size.y}");
    }
    

    private void SetTicketVisual(OrderTicket t, Vector2 size, float scale)
    {
        var rt = (RectTransform)t.transform;

        var le = t.GetComponent<LayoutElement>();
        if (le)
        {
            le.preferredWidth = size.x;
            le.preferredHeight = size.y;
            le.minWidth = size.x;
            le.minHeight = size.y;
            le.flexibleWidth = 0f;
            le.flexibleHeight = 0f;
        }
        else
        {
            rt.sizeDelta = size;
        }

        rt.localScale = Vector3.one * scale;
    }

    public OrderTicket GetCurrentDetailTicket()
    {
        if (currentDetailOrder.HasValue &&
            tickets.TryGetValue(currentDetailOrder.Value, out var t))
        {
            return t;
        }

        return null;
    }

    private void SetDetailOrder(int? orderNumber)
    {
        currentDetailOrder = orderNumber;
        OnDetailOrderChanged?.Invoke(currentDetailOrder);
    }
    
    public OrderTicket GetTicket(int orderNumber)
    {
        tickets.TryGetValue(orderNumber, out var t);
        return t;
    }

    public void ApplyCatToTicket(int orderNumber, CatDefinition cat)
    {
        var t = GetTicket(orderNumber);
        if (t)
        {
            //t.SetCat(cat);
            Debug.Log($"[TicketBoard] Applied cat '{cat.catName}' to order {orderNumber}");
        }
        else
        {
            Debug.LogWarning($"[TicketBoard Ticket #{orderNumber} not found when applying cat");
        }
    }
    
    
}
