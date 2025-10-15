using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SyrupAnimator : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image syrupImage;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite pumpDownSprite;

    [Header("Animation Settings")]
    [SerializeField] private float pumpDuration = 0.2f;
    [SerializeField] private float pumpAngleChange = 10f; 

    [Header("Pump Settings")]
    [SerializeField] private int totalPumps = 3;
    private int currentPumps = 0;
    private bool isLocked = false;

    private RectTransform rt;
    private SyrupType syrupType = SyrupType.None;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void SetSyrupType(SyrupType type) => syrupType = type;

    public void SetTotalPumps(int pumps)
    {
        totalPumps = Mathf.Clamp(pumps, 1, 3);
        currentPumps = 0; // reset
        isLocked = false;
    }

    public void OnClickPump()
    {
        if (isLocked) return;

        currentPumps++;
        StartCoroutine(PumpOnce());

        if (currentPumps >= totalPumps)
        {
            isLocked = true;
        }
    }

    private IEnumerator PumpOnce()
    {
        // Change sprite to pump down
        if (syrupImage && pumpDownSprite)
            syrupImage.sprite = pumpDownSprite;

        Quaternion startRot = rt.localRotation;
        
        float currentZ = startRot.eulerAngles.z;
        if (currentZ > 180f) currentZ -= 360f;
        
        Quaternion pumpRot = Quaternion.Euler(0f, 0f, currentZ - pumpAngleChange);

        // Pump down
        float t = 0f;
        while (t < pumpDuration)
        {
            t += Time.deltaTime;
            rt.localRotation = Quaternion.Slerp(startRot, pumpRot, t / pumpDuration);
            yield return null;
        }

        // Pump back up
        t = 0f;
        while (t < pumpDuration)
        {
            t += Time.deltaTime;
            rt.localRotation = Quaternion.Slerp(pumpRot, startRot, t / pumpDuration);
            yield return null;
        }

        rt.localRotation = startRot;

        // Change sprite back to normal
        if (syrupImage && normalSprite)
            syrupImage.sprite = normalSprite;
    }

    public bool IsComplete()
    {
        return currentPumps >= totalPumps;
    }

    public void ResetPumps()
    {
        currentPumps = 0;
        isLocked = false;
        
        if (syrupImage && normalSprite)
            syrupImage.sprite = normalSprite;
    }
}