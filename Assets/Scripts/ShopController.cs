using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : SingletonMonoBehaviour<ShopController>
{
    [SerializeField] private List<Slot> shopSlots = new List<Slot>();
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private PopUp popUp;
    public Action onOpenShop;
    public Action onCloseShop;

    public bool isOpen;
        
    private void Start()
    {
        onOpenShop += OpenShop;
        onCloseShop += CloseShop;
        SetupShop();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onOpenShop -= OpenShop;
        onCloseShop -= CloseShop;
    }

    public void SetupShop()
    {
        foreach(Slot slot in shopSlots)
        {
            if(slot.clothe == null)
            {
                Debug.Log("Check Slot's clothe");
                return;
            }
            slot.slotImage.sprite = slot.clothe.sprite;
            slot.label.text = slot.clothe.price.ToString();
            slot.title = slot.clothe.title;
        }
    }    
    public void BuyClothe(Clothe clothe)
    {
        InventoryController.instance.AddClothe(clothe);
        InventoryController.instance.playerMoney -= clothe.price;
        InventoryController.instance.UpdatePlayerMoney();
    }

    public void SellClothe(Clothe clothe)
    {
        Slot slot = InventoryController.instance.GetSlotWithClothe(clothe);
        if(slot != null)
        {
            if (--slot.clothesCount == 0)
            {
                if (slot.isBeingUsed)
                {
                    ClotheController.instance.RemoveClothe(clothe);
                    slot.isBeingUsed = false;
                    slot.UpdateSlotColor();
                }
                slot.ClearSlot();
            }
            InventoryController.instance.UpdatePlayerMoney();
        }

    }

    public void OpenPopUp(Slot shopSlot)
    {
        popUp.SetPopUp(shopSlot);   
    }

    public void OpenShop()
    {
        if (isOpen) return;
        isOpen = true;
        shopPanel.SetActive(true);
        onOpenShop?.Invoke();
    }

    public void CloseShop()
    {
        if (!isOpen) return;
        isOpen = false;
        shopPanel.SetActive(false);
        onCloseShop?.Invoke();
    }

    

}
