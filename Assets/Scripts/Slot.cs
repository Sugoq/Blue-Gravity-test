using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Slot : MonoBehaviour
{
    public UnityEvent onStart;
    [HideInInspector] public int clothesCount = 0;
    public Clothe clothe;
    public Image slotImage;
    [SerializeField] private Image slotBackground;
    [SerializeField] private Color usingClotheColor;
    [SerializeField] private Color notUsingClotheColor;
    public bool isBeingUsed;

    public bool hasClothe => clothe != null;
    public TMP_Text label;
    public string title;

    [Header("Check if its a invetory slot")]
    [SerializeField] private bool isInventorySlot;

    [Header("If its being used as an inventory slot, value will be the count, otherwise it will be the price")]
    public int value;

    public void SetTextAsPrice() => label.text = $"{clothe.price}";

    public void UpdateInventorySlot()
    {
        if (clothe == null) return;
        slotImage.color = Color.white;
        slotImage.sprite = clothe.sprite;
        title = clothe.title;
        label.text = $"{clothesCount}";
    }

    public void EquipClothe()
    {
        if (clothe == null) return;
        if (!isBeingUsed)
        {
            InventoryController.instance.DisableClothesOfType(clothe.clotheType);
            ClotheController.instance.AddClothe(clothe);
        }
        else
        {
            ClotheController.instance.RemoveClothe(clothe);
        }
        isBeingUsed = !isBeingUsed;
        UpdateSlotColor();
    }

    public void UpdateSlotColor()
    {
        
        if (clothe == null || !isBeingUsed)
            slotBackground.color = notUsingClotheColor;
        else
            slotBackground.color = usingClotheColor;
    }

    public void ClearSlot()
    {
        clothe = null;
        clothesCount = 0;
        isBeingUsed = false;
        slotImage.color = Color.clear;
        title = string.Empty;
        label.text = $"";
        UpdateSlotColor();
    }

    public void PopUpClothe()
    {
        ShopController.instance.OpenPopUp(this);   
    }

    public void SetupPreview()
    {
        PreviewController.instance.PreviewClothe(clothe);
    }

    public void ClearPreview()
    {
        PreviewController.instance.Clear();
    }

}
