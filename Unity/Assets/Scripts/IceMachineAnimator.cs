using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class IceMachineAnimator : MonoBehaviour
{
    public RectTransform machineRect;
    public float slideDuration = 0.5f;
    public float slideOffsetY = 500f; // how far above the screen it starts

    private Vector2 startPos; // off screen position
    private Vector2 endPos;  
    
    private ILogger logger = new DebugLogger();

    private void Awake()
    {
        if (machineRect == null)
            machineRect = GetComponent<RectTransform>();

        endPos = machineRect.anchoredPosition;
        startPos = endPos + new Vector2(0, slideOffsetY);
        machineRect.anchoredPosition = startPos;
    }

    public void SlideIn()
    {
        StopAllCoroutines();
        StartCoroutine(Slide(machineRect.anchoredPosition, endPos));
    }

    public void SlideOut()
    {
        StopAllCoroutines();
        StartCoroutine(Slide(machineRect.anchoredPosition, startPos));
    }

    private IEnumerator Slide(Vector2 from, Vector2 to)
    {
        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            machineRect.anchoredPosition = Vector2.Lerp(from, to, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        machineRect.anchoredPosition = to;
    }
}