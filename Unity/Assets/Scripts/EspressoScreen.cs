using UnityEngine;

public class EspressoScreen : MonoBehaviour
{
    public tempDrink drink;
    public GameObject startButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drink.SetIsHot(true);   // i just want to start with hot drinks for now idk
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
