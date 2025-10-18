using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// basically all ChatGPT bc like idk this already took like hours

public class TicketBoard : MonoBehaviour
{
    [Header("Prefabs & Parents")]
    [SerializeField] private OrderTicket ticketPrefab;
    [SerializeField] private RectTransform topRowParent;
    [SerializeField] private RectTransform detailParent;
    
    [Header("Exact Sizes")]
    [SerializeField] private Vector2 largeSize = new Vector2(500f, 700f);
    //[SerializeField] private float largeScale = 1f;
    [SerializeField] private Vector2 smallSize = new Vector2(150f, 250f);
    //[SerializeField] private float smallScale = 1f;

    [Header("Content Scales")]
    [SerializeField] private float largeContentScale = 1f;
    [SerializeField] private float smallContentScale = 0.5f;

    private readonly Dictionary<int, OrderTicket> tickets = new Dictionary<int, OrderTicket>();
    private int? currentDetailOrder = null;
    
    public void SpawnTicket(int orderNumber, OrderTicketData data)
    {
        if (!ticketPrefab || !topRowParent || !detailParent)
        {
            Debug.LogError("[TicketBoard] Assign ticketPrefab, topRowParent, detailParent");
            return;
        }
        
        // First ticket appears big, others go to top row
        bool spawnInDetail = currentDetailOrder == null;
        RectTransform parent = spawnInDetail ? detailParent : topRowParent;

        // Create ticket
        var ticket = Instantiate(ticketPrefab, parent);
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

        // Size + track
        if (spawnInDetail)
        {
            currentDetailOrder = orderNumber;
            CenterInParent(rt);
            SetTicketSize(rt, largeSize);
            ticket.SetContentScale(largeContentScale);
            //SetTicketVisual(ticket, largeSize, largeScale);
        }
        else
        {
            SetTicketSize(rt, smallSize);
            ticket.SetContentScale(smallContentScale);
            //SetTicketVisual(ticket, smallSize, smallScale);
            ticket.transform.SetAsLastSibling();
        }
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
    }

    private void MoveToTopRow(OrderTicket t)
    {
        //var rt = (RectTransform)t.transform;
        t.transform.SetParent(topRowParent, false);
        var rt = (RectTransform)t.transform;
       // Normalize((RectTransform)t.transform);
        //rt.localScale = Vector3.one;
        //SetTicketVisual(t, smallSize, smallScale);
        SetTicketSize(rt, smallSize);
        t.SetContentScale(smallContentScale);
        t.transform.SetAsLastSibling();
    }
    
    private void MoveToDetail(OrderTicket t)
    {
        //var rt = (RectTransform)t.transform;
        t.transform.SetParent(detailParent, false);
        var rt = (RectTransform)t.transform;
        CenterInParent(rt);
        SetTicketSize(rt, largeSize);
        //SetTicketVisual(t, largeSize, largeScale);
        t.SetContentScale(largeContentScale);
        //t.transform.SetAsLastSibling();
    }
    
    private void CenterInParent(RectTransform rt)
    {
        // Center anchors/pivot and zero local position so it sits in the middle of the parent
        //rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        if (!rt) return;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
        //rt.localScale = Vector3.one;
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
        
        Debug.Log($"SetTicketSize called for {rt.name}: target size = {size.x} x {size.y}");
    }

    /*
    private void SetTicketSize(OrderTicket t, Vector2 size)
    {
        var le = t.GetComponent<LayoutElement>();
        if (le)
        {
            le.preferredWidth = size.x;
            le.preferredHeight = size.y;
            le.minWidth = size.x;
            le.minHeight = size.y;
        }
        else
        {
            ((RectTransform)t.transform).sizeDelta = size;
        }
    }
    */

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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
