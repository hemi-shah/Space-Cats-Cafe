using UnityEngine;
using UnityEngine.UI;

public class CustomerCat : MonoBehaviour
{
    [Header("UI")] [SerializeField] private Image portrait;
    [SerializeField] private Text nameText;
    [SerializeField] private Button takeOrderButton;

    private CatDefinition cat;
    private SeatingManager seating;
    private int seatIndex = -1;

    private CustomerManager customerManager;
    private ScreenManager screenManager;

    public void Init(SeatingManager seatingManager, int seatIdx, CatDefinition catDef,
        CustomerManager cm, ScreenManager sm)
    {
        seating = seatingManager;
        seatIndex = seatIdx;
        cat = catDef;
        customerManager = cm;
        screenManager = sm;

        if (portrait) portrait.sprite = cat ? cat.catSprite : null;
        if (nameText) nameText.text = cat ? cat.catName : "";

        if (takeOrderButton)
        {
            takeOrderButton.onClick.RemoveAllListeners();
            takeOrderButton.onClick.AddListener(OnTakeOrderClicked);
        }
    }

    private void OnTakeOrderClicked()
    {
        if (!cat || !customerManager || !screenManager || seating == null || seatIndex < 0) 
            return;

        int orderNum = customerManager.TakeOrderForCat(cat);

        gameObject.SetActive(false);

        seating.OnSeatTaken(seatIndex, orderNum);

        screenManager.NavigateTo("TakeOrderScreen");

    }
}
