using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// THIS IS AI GENERATED (I don't know how to do animations)
[RequireComponent(typeof(RectTransform))]
public class CupAnimator : MonoBehaviour, IDragHandler
{
    public RectTransform cupRect;
    public float slideDuration = 0.5f;

    public Vector2 _startPos;
    public Vector2 CenterPos;
    private RectTransform _canvasRect;

    [Header("Cup UI Images")]
    public Sprite cupImage;
    public Sprite cupWithOneIce;
    public Sprite cupWithTwoIce;
    public Sprite cupWithThreeIce;

    [Header("Drag Settings")]
    public float minX = -200f;
    public float maxX = 200f;
    private bool _isDragging = false;
    private bool _canDrag = true;   // ← NEW

    private void Awake()
    {
        if (cupRect == null)
            cupRect = GetComponent<RectTransform>();

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
            _canvasRect = canvas.GetComponent<RectTransform>();

        _startPos = cupRect.anchoredPosition;
        CenterPos = new Vector2(0, _startPos.y);
    }

    // ---------------- Slide Animations ----------------
    public void SlideToCenter()
    {
        StopAllCoroutines();
        StartCoroutine(SlideCup(cupRect.anchoredPosition, CenterPos));
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
            if (!_isDragging)
                cupRect.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / slideDuration);

            elapsed += Time.deltaTime;
            yield return null;
        }
        if (!_isDragging)
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

    // ---------------- Drag Behavior ----------------
    public void OnDrag(PointerEventData eventData)
    {
        if (!_canDrag) return; // ← NEW check

        _isDragging = true;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasRect,
            eventData.position,
            _canvasRect.GetComponent<Canvas>().worldCamera,
            out pos
        );

        cupRect.anchoredPosition = new Vector2(
            Mathf.Clamp(pos.x, minX, maxX),
            cupRect.anchoredPosition.y
        );
    }

    private void LateUpdate()
    {
        if (_isDragging)
            _isDragging = false;
    }

    // ---------------- Public Controls ----------------
    public void SetDraggable(bool canDrag)
    {
        _canDrag = canDrag;
    }
}