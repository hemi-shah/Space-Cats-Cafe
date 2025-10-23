using UnityEngine;

public class IceCube : MonoBehaviour
{
    public float fallSpeed = 300f; 
    private RectTransform rect;
    private RectTransform cupRect;
    private IceGameMechanics gameMechanics;

    public void Init(RectTransform cup, IceGameMechanics mechanics)
    {
        rect = GetComponent<RectTransform>();
        cupRect = cup;
        gameMechanics = mechanics;
    }

    private void Update()
    {
        if (rect == null || cupRect == null || gameMechanics == null) return;

        rect.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;

        if (RectOverlaps(rect, cupRect))
        {
            gameMechanics.OnIceCaught();
            Destroy(gameObject);
        }

        if (rect.anchoredPosition.y < -1000)
        {
            Destroy(gameObject);
        }
    }

    private bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Rect aWorld = GetWorldRect(a);
        Rect bWorld = GetWorldRect(b);
        return aWorld.Overlaps(bWorld);
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector2 size = new Vector2(
            corners[2].x - corners[0].x,
            corners[2].y - corners[0].y
        );
        return new Rect(corners[0], size);
    }
}