using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SyrupSelection : MonoBehaviour
{
    [Header("Syrup GameObjects")]
    public GameObject caramelSyrup;
    public GameObject chocolateSyrup;
    public GameObject mochaSyrup;

    [Header("Animators")]
    public SyrupAnimator caramelSyrupAnimator;
    public SyrupAnimator chocolateSyrupAnimator;
    public SyrupAnimator mochaSyrupAnimator;

    [Header("Animation Settings")]
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float tiltAngle = 65f;
    [SerializeField] private float tiltDelay = 0.3f;

    [Header("Drink References")] 
    public DrinkManager drinkManager;
    public GameObject hotDrinkImage;
    public GameObject coldDrinkImage;

    //private Drink currentDrink;
    private NewDrink activeDrink;
    private bool selected = false;
    
    private Vector2 caramelOriginalPos;
    private Vector2 chocolateOriginalPos;
    private Vector2 mochaOriginalPos;
    
    private RectTransform _canvasRect;

    private void Start()
    {
        //currentDrink = new Drink(isHot: false);
        activeDrink = drinkManager.GetActiveDrink();

        if (activeDrink == null)
        {
            return;
        }
        //UpdateDrinkImage();
        
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            _canvasRect = canvas.GetComponent<RectTransform>();
        
        StoreOriginalPositions();
    }

    // THIS IS AI GENERATED
    private void StoreOriginalPositions()
    {
        if (caramelSyrup != null)
        {
            var rt = caramelSyrup.GetComponent<RectTransform>();
            caramelOriginalPos = rt.anchoredPosition;
        }
        if (chocolateSyrup != null)
        {
            var rt = chocolateSyrup.GetComponent<RectTransform>();
            chocolateOriginalPos = rt.anchoredPosition;
        }
        if (mochaSyrup != null)
        {
            var rt = mochaSyrup.GetComponent<RectTransform>();
            mochaOriginalPos = rt.anchoredPosition;
        }
    }

    public void SelectCaramelSyrup()
    {
        if (selected) return;
        selected = true;
        
        Debug.Log("Caramel syrup selected!");
        
        StartCoroutine(HandleSelectedPump(caramelSyrup, caramelSyrupAnimator, SyrupType.Caramel, 3));
    }

    public void SelectChocolateSyrup()
    {
        if (selected) return;
        selected = true;
        
        Debug.Log("Chocolate syrup selected!");
        
        StartCoroutine(HandleSelectedPump(chocolateSyrup, chocolateSyrupAnimator, SyrupType.Chocolate, 2));
    }

    public void SelectMochaSyrup()
    {
        if (selected) return;
        selected = true;
        
        Debug.Log("Mocha syrup selected!");
        
        StartCoroutine(HandleSelectedPump(mochaSyrup, mochaSyrupAnimator, SyrupType.Mocha, 1));
    }

    private IEnumerator HandleSelectedPump(GameObject selectedSyrup, SyrupAnimator animator, SyrupType syrupType, int pumps)
    {
        var rt = selectedSyrup.GetComponent<RectTransform>();
        if (rt == null) yield break;

        // Setup anchors for predictable movement
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        animator.SetTotalPumps(pumps);
        animator.SetSyrupType(syrupType);
        
        yield return new WaitForSeconds(tiltDelay);
        yield return StartCoroutine(TiltPump(rt, tiltAngle));
    }

    private IEnumerator TiltPump(RectTransform rt, float targetAngle)
    {
        if (rt == null) yield break;

        Quaternion startRot = rt.localRotation;
        Quaternion targetRot = Quaternion.Euler(0f, 0f, -targetAngle);
        
        float duration = 0.4f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            rt.localRotation = Quaternion.Slerp(startRot, targetRot, elapsed / duration);
            yield return null;
        }

        rt.localRotation = targetRot;
    }

    // should find a way to call this after your drink moves from this station
    public void ResetForNextDrink()
    {
        selected = false;
        
        if (caramelSyrup != null)
            StartCoroutine(ResetPump(caramelSyrup.GetComponent<RectTransform>(), caramelOriginalPos));
        if (chocolateSyrup != null)
            StartCoroutine(ResetPump(chocolateSyrup.GetComponent<RectTransform>(), chocolateOriginalPos));
        if (mochaSyrup != null)
            StartCoroutine(ResetPump(mochaSyrup.GetComponent<RectTransform>(), mochaOriginalPos));
    }

    private IEnumerator ResetPump(RectTransform rt, Vector2 originalPos)
    {
        if (rt == null) yield break;

        // Reset rotation first
        Quaternion startRot = rt.localRotation;
        float duration = 0.4f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            rt.localRotation = Quaternion.Slerp(startRot, Quaternion.identity, elapsed / duration);
            yield return null;
        }
        rt.localRotation = Quaternion.identity;
    }

    /*
    private void UpdateDrinkImage()
    {
        if (activeDrink == null) return;

        bool isHot = activeDrink.IsHot;
        hotDrinkImage.SetActive(isHot);
        coldDrinkImage.SetActive(!isHot);
    }
    */
}