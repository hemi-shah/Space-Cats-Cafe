using System.Collections;
using UnityEngine;

// lots of ChatGPT here for Coroutines and movement bc idk how to do it

public class MilkScreen : MonoBehaviour
{
    public tempDrink drink;
    public GameObject dairyMilk;
    public GameObject almondMilk;
    public GameObject oatMilk;

    [SerializeField] private Vector2 teleportAnchoredPos = new Vector2(-360f, 180f);
    [SerializeField] private float teleportTiltZ = 65f;
    [SerializeField] private RectTransform cup;             // drag your cup UI object here
    [SerializeField] private Vector2 offsetFromCup = new Vector2(-140f, 180f); 
    [SerializeField] private float postTeleportDelay = 1.5f;

    public MilkType SelectedMilk { get; private set; } = MilkType.None;

    private Coroutine active;

    void Start()
    {
        // testing with hot non empty drink
        drink.SetIsHot(true);
        drink.SetIsEmpty(false);
    }
    
    public void SelectDairy()
    {
        Select(MilkType.Dairy);
    }

    public void SelectAlmond()
    {
        Select(MilkType.Almond);
    }

    public void SelectOat()
    {
        Select(MilkType.Oat);
    }

    private void Select(MilkType type)
    {
        SelectedMilk = type;

        dairyMilk.SetActive(type == MilkType.Dairy);
        almondMilk.SetActive(type == MilkType.Almond);
        oatMilk.SetActive(type == MilkType.Oat);

        GameObject chosen =
            type == MilkType.Dairy ? dairyMilk : 
                type == MilkType.Almond ? almondMilk :
                    type == MilkType.Oat ? oatMilk : null;

        if (chosen == null) return;

        if (active != null) return;
        active = StartCoroutine(TeleportThenDelay(chosen));
    }

    // this function is ChatGPT
    private IEnumerator TeleportThenDelay(GameObject milkGo)
    {
        /*
        var rt = milkGo.GetComponent<RectTransform>();
        if (!rt) yield break;
        
        // Teleport
        rt.anchoredPosition = teleportAnchoredPos;
        var e = rt.localEulerAngles;
        e.z = -Mathf.Abs(teleportTiltZ);
        rt.localEulerAngles = e;
        
        yield return new WaitForSeconds(postTeleportDelay);

        drink.PourMilk(SelectedMilk);
        */
        var rt = milkGo.GetComponent<RectTransform>();
        if (!rt || !cup) yield break;

        // Make coords predictable
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        // Compute target anchoredPosition relative to the milk's parent
        Vector2 targetAnchored;
        var parentRT = (RectTransform)rt.parent;

        if (cup.transform.IsChildOf(parentRT)) {
            // Same parent space: anchored positions are compatible
            targetAnchored = cup.anchoredPosition + offsetFromCup;
        } else {
            // Different parents: convert cup world → parent local → anchored
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRT,
                RectTransformUtility.WorldToScreenPoint(null, cup.position),
                null,
                out targetAnchored
            );
            targetAnchored += offsetFromCup;
        }

        // Teleport & tilt
        rt.anchoredPosition = targetAnchored;
        rt.localRotation = Quaternion.Euler(0f, 0f, -Mathf.Abs(teleportTiltZ)); // tilt left (or use + for right)

        // Pause, then apply milk
        yield return new WaitForSeconds(postTeleportDelay);
        drink.PourMilk(SelectedMilk);
        
    }
}
