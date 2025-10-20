using System.Collections.Generic;
using UnityEngine;

public class SeatingManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private CatCatalog catalog;
    [SerializeField] private CustomerCat customerCardPrefab;
    [SerializeField] private RectTransform spawnParent; // panel under OrderPageScreen Canvas

    [Header("Seat positions (anchored)")]
    [SerializeField] private Vector2[] seatPositions =
    {
        new Vector2(-235f, 198f),
        new Vector2(  25f, 140f),
        new Vector2( 425f,  67f),
    };

    [Header("References")]
    [SerializeField] private CustomerManager customerManager;
    [SerializeField] private ScreenManager screenManager;

    private readonly Dictionary<int, CustomerCat> seatToCard = new();     // seatIndex -> card
    private readonly Dictionary<int, int> orderToSeat = new();             // orderNumber -> seatIndex

    // anti-repeat bag
    private List<CatDefinition> bag;

    private void Awake()
    {
        if (!spawnParent) spawnParent = (RectTransform)transform;
    }

    private void Start()
    {
        RefillBag();
        FillAllSeats();
    }

    private void RefillBag()
    {
        bag = new List<CatDefinition>(catalog ? catalog.cats : new List<CatDefinition>());
        // simple shuffle
        for (int i = 0; i < bag.Count; i++)
        {
            int j = Random.Range(i, bag.Count);
            (bag[i], bag[j]) = (bag[j], bag[i]);
        }
    }

    private CatDefinition NextFromBag()
    {
        if (bag == null || bag.Count == 0) RefillBag();
        int last = bag.Count - 1;
        var cat = bag[last];
        bag.RemoveAt(last);
        return cat;
    }

    private void FillAllSeats()
    {
        for (int i = 0; i < seatPositions.Length; i++)
        {
            SpawnAtSeat(i);
        }
    }

    private void SpawnAtSeat(int seatIndex)
    {
        if (!customerCardPrefab || !spawnParent) return;

        // if there's already a card object for this seat (from previous order), destroy it first
        if (seatToCard.TryGetValue(seatIndex, out var existing) && existing)
        {
            Destroy(existing.gameObject);
            seatToCard.Remove(seatIndex);
        }

        var card = Instantiate(customerCardPrefab, spawnParent);
        var rt = (RectTransform)card.transform;
        rt.anchorMin = rt.anchorMax = rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = seatPositions[seatIndex];
        rt.localScale = Vector3.one;

        var randomCat = NextFromBag();
        card.Init(this, seatIndex, randomCat, customerManager, screenManager);
        seatToCard[seatIndex] = card;
    }

    // Called by the CustomerCard when the player takes the order
    public void OnSeatTaken(int seatIndex, int orderNumber)
    {
        orderToSeat[orderNumber] = seatIndex;
        // The card is already SetActive(false) in CustomerCard.OnTakeOrderClicked
        // We keep it hidden here until the order is completed
        Debug.Log($"[Seating] Seat {seatIndex} now tied to order #{orderNumber}");
    }

    // Called when the order is actually completed (served)
    public void OnOrderCompleted(int orderNumber)
    {
        if (!orderToSeat.TryGetValue(orderNumber, out var seatIndex))
            return;

        orderToSeat.Remove(orderNumber);

        // Destroy the old (hidden) card, and spawn a new random cat in that seat
        if (seatToCard.TryGetValue(seatIndex, out var card) && card)
        {
            Destroy(card.gameObject);
            seatToCard.Remove(seatIndex);
        }

        SpawnAtSeat(seatIndex);
    }
}
