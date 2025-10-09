using UnityEngine;
using System.Collections;

// THIS CLASS WAS AI GENERATED (I can't do math help)
[RequireComponent(typeof(RectTransform))]
public class CupAnimator : MonoBehaviour
{
    public RectTransform cupRect;
    public float slideDuration = 0.5f;

    private Vector2 _startPos;
    private Vector2 _centerPos;
    private RectTransform _canvasRect;

    private void Awake()
    {
        if (cupRect == null)
            cupRect = GetComponent<RectTransform>();

        // Find parent canvas RectTransform
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            _canvasRect = canvas.GetComponent<RectTransform>();

        _startPos = cupRect.anchoredPosition;
        _centerPos = new Vector2(0, _startPos.y);
    }

    public void SlideToCenter()
    {
        StopAllCoroutines();
        StartCoroutine(SlideCup(cupRect.anchoredPosition, _centerPos));
    }

    public void SlideOutLeft()
    {
        StopAllCoroutines();
        StartCoroutine(SlideCup(cupRect.anchoredPosition, GetOffscreenLeft()));
    }

    public void SlideOutRight()
    {
        StopAllCoroutines();
        StartCoroutine(SlideCup(cupRect.anchoredPosition, GetOffscreenRight()));
    }

    private IEnumerator SlideCup(Vector2 startPos, Vector2 endPos)
    {
        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            cupRect.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cupRect.anchoredPosition = endPos;
    }

    private Vector2 GetOffscreenLeft()
    {
        if (_canvasRect == null) return _startPos + new Vector2(-500f, 0);
        float offscreenX = -_canvasRect.rect.width / 2 - cupRect.rect.width;
        return new Vector2(offscreenX, _startPos.y);
    }

    private Vector2 GetOffscreenRight()
    {
        if (_canvasRect == null) return _startPos + new Vector2(500f, 0);
        float offscreenX = _canvasRect.rect.width / 2 + cupRect.rect.width;
        return new Vector2(offscreenX, _startPos.y);
    }
}
