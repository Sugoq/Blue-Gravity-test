using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class PopUp : SingletonMonoBehaviour<PopUp>
{
    public UnityEvent<Clothe> onBuy;
    public UnityEvent<Clothe> onSell;

    [SerializeField] private TMP_Text label;
    [SerializeField] private TMP_Text clotheName;
    [SerializeField] private Text errorText;
    [SerializeField] private Image clotheSprite;
    [HideInInspector] public Clothe clothe;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
    private Slot currentSlot;

    [Header("Error Messages")]
    public string noMoney;
    public string fullInventory;
    public string clotheLimit;

    public void SetPopUp(Slot slot)
    {
        currentSlot = slot;

        buyButton.interactable = true;
        sellButton.interactable = true;

        clotheSprite.sprite = slot.clothe.sprite;
        clotheName.text = slot.clothe.title;
        clothe = slot.clothe;
        label.text = $"${slot.clothe.price}";
        if (!InventoryController.instance.CanAddClothe(clothe, out var error))
        {
            buyButton.interactable = false;
            errorText.text = error.Value;
        }
        else
        {
            errorText.text = "";
        }
        sellButton.interactable = InventoryController.instance.HasClothe(clothe);
    }

    public void Buy()
    {
        onBuy?.Invoke(clothe);
        SetPopUp(currentSlot);
    }
    public void Sell()
    {
        onSell?.Invoke(clothe);
        SetPopUp(currentSlot);
    }
}
public class PopUpError
{
    private PopUpError(string value) { Value = value; }

    public string Value { get; private set; }

    public static PopUpError INVENTORY_FULL { get { return new PopUpError(PopUp.instance.fullInventory); } }
    public static PopUpError NO_MONEY { get { return new PopUpError(PopUp.instance.noMoney); } }
    public static PopUpError CLOTHE_LIMIT { get { return new PopUpError(PopUp.instance.clotheLimit); } }
    public static PopUpError NO_ERROR { get { return new PopUpError(""); } }
    
}
