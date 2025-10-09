using UnityEngine;

public class ChangeScreenButton : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject nextPanel;
    
    public void switchScreen()
    {
        if (currentPanel != null && nextPanel != null)
        {
            currentPanel.SetActive(false);
            nextPanel.SetActive(true);
        }
    }
}
