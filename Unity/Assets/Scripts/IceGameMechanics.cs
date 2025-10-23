using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IceGameMechanics : MonoBehaviour
{
    [Header("References")]
    public IceMachineAnimator iceMachineAnimator; 
    public CupAnimator cupAnimator;               
    public GameObject icePrefab;
    public Transform iceSpawnPoint; 
    public DrinkManager drinkManager;

    [Header("Settings")]
    public int requiredIce = 3;
    public float iceFallInterval = 1.5f;
    public float delayBeforeSpawning = 1.0f; 

    [Header("Cup Settings")]
    public Vector2 cupStartPos = new Vector2(-200, -300); 

    [Header("Ice Spawn Randomization")]
    public float minXSpawn = -300f; // leftmost spawn position
    public float maxXSpawn = 300f;  // rightmost spawn position

    private int iceCounter = 0;
    private bool isPlaying = false;

    private void OnEnable()
    {
        iceCounter = 0;
        isPlaying = false;
        
        if (cupAnimator != null && cupAnimator.cupRect != null)
        {
            cupAnimator.cupRect.anchoredPosition = cupStartPos;
        }
        
        if (iceMachineAnimator != null)
        {
            iceMachineAnimator.SlideIn();
        }
    }

    private void Start()
    {
        StartMiniGame();
    }

    public void StartMiniGame()
    {
        if (isPlaying) return;
        isPlaying = true;

        if (cupAnimator != null)
            cupAnimator.SetDraggable(true); 

        StartCoroutine(SpawnIceRoutine());
    }

    private IEnumerator SpawnIceRoutine()
    {
        yield return new WaitForSeconds(delayBeforeSpawning);
        while (iceCounter < requiredIce)
        {
            SpawnIce();
            yield return new WaitForSeconds(iceFallInterval);
        }
        Debug.Log("Finished spawning all ice cubes");
    }

    private void SpawnIce()
    {
        
        GameObject ice = Instantiate(icePrefab, iceSpawnPoint.parent);
        
        IceCube iceScript = ice.GetComponent<IceCube>();
        
        BoxCollider2D iceCollider = ice.GetComponent<BoxCollider2D>();

        iceCollider.isTrigger = true;
        Debug.Log("Ice has BoxCollider2D, isTrigger: " + iceCollider.isTrigger);

        RectTransform iceRect = ice.GetComponent<RectTransform>();
        if (iceRect != null)
        {
            RectTransform spawnRectTransform = iceSpawnPoint.GetComponent<RectTransform>();
            if (spawnRectTransform != null)
            {
                float randomX = Random.Range(minXSpawn, maxXSpawn);
                Vector2 spawnPos = spawnRectTransform.anchoredPosition;
                spawnPos.x = randomX;
                
                iceRect.anchoredPosition = spawnPos;
            }
            iceRect.localScale = Vector3.one;
        }

        if (iceScript != null)
        {
            iceScript.Init(cupAnimator.cupRect, this);
        }
    }

    public void OnIceCaught()
    {
        iceCounter++;
        Debug.Log("Ice caught! Total: " + iceCounter);

        if (cupAnimator != null && cupAnimator.cupRect != null)
        {
            Image cupImg = cupAnimator.cupRect.GetComponent<Image>();
            
            if (cupImg != null)
            {
                switch (iceCounter)
                {
                    case 1:
                        if (cupAnimator.cupWithOneIce != null)
                        {
                            cupImg.sprite = cupAnimator.cupWithOneIce;
                        }
                        break;
                    case 2:
                        if (cupAnimator.cupWithTwoIce != null)
                        {
                            cupImg.sprite = cupAnimator.cupWithTwoIce;
                        }
                        break;
                    case 3:
                        if (cupAnimator.cupWithThreeIce != null)
                        {
                            cupImg.sprite = cupAnimator.cupWithThreeIce;
                        }
                        break;
                }
            }
        }

        if (iceCounter >= requiredIce)
        {
            Debug.Log("Required ice reached! Ending minigame...");
            EndMiniGame();
        }
    }

    private void EndMiniGame()
    {
        isPlaying = false;

        if (iceMachineAnimator != null)
            iceMachineAnimator.SlideOut();

        if (cupAnimator != null)
        {
            cupAnimator.SetDraggable(false);  
            cupAnimator.SlideToCenter();  
        }

        Debug.Log(iceCounter + " cubes caught. Ice mini-game complete!");

        if (drinkManager != null)
        {
            NewDrink createdDrink = drinkManager.CreateDrink(TemperatureType.Cold, iceCounter);
            Debug.Log("Drink created by IceGameMechanics via DrinkManager ");
        }
    }
}