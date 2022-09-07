using System.Collections.Generic;
using UnityEngine;

public class InventoryController : SingletonMonoBehaviour<InventoryController>
{
    [SerializeField] private List<Slot> slots = new List<Slot>();
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TMPro.TMP_Text moneyText; 
    [SerializeField] private int storageCapacity;
    public int playerMoney;
    private bool isInventoryOpen;

    private void Start()
    {
        UpdatePlayerMoney();
    }

    public void LoadSlots(List<int> list)
    {
        int index = 0;
        for(int i =0; i< list.Count; i++)
        {
            //empty if its greater than 500
            if(list[i] > 500) slots[index].ClearSlot();           
            else
            {
                slots[index].clothe = MemoryController.instance.GetClotheById(list[i]);
                slots[index].clothesCount = list[++i];
                slots[index].UpdateInventorySlot();
            }
            index++;
        }
        foreach(Clothe clothe in ClotheController.instance.clothes)
        {
            Slot slot = slots.Find(x => x.clothe.id == clothe.id);
            slot.isBeingUsed = true;
            slot.UpdateSlotColor();
        }
    }

    public string GetInventoryString()
    {
        string s = string.Empty;
        for (int i = 0; i< slots.Count; i++)
        {
            if (slots[i].clothe != null)
            {
                s += $"{slots[i].clothe.id},{slots[i].clothesCount}";
            }
            else s += "1000";
            if (i < slots.Count - 1)
                s += "-";
        }
        return s;
    }

    public void AddClothe(Clothe clothe)
    {
        Slot slot = slots.Find(x => x.hasClothe && x.clothe.id == clothe.id);
        
        if(slot != null)
        {
            slot.clothesCount++;
            slot.UpdateInventorySlot();
            MemoryController.instance.SaveInventory();
            return;
        }
        Slot emptySlot = slots.Find(x => !x.hasClothe);
        emptySlot.clothe = clothe;
        emptySlot.clothesCount++;
        emptySlot.UpdateInventorySlot();
        
        MemoryController.instance.SaveInventory();
    }

    public Slot GetSlotWithClothe(Clothe clothe) => slots.Find(x => x.hasClothe && x.clothe.id == clothe.id);

    public bool CanAddClothe(Clothe clothe, out PopUpError error)
    {
        if (playerMoney < clothe.price)
        {
            error = PopUpError.NO_MONEY;
            return false;
        }
        Slot slot = slots.Find(x => x.hasClothe && x.clothe.id == clothe.id);
        if (slot != null)
        {
            if (slot.clothesCount >= storageCapacity)
            {
                error = PopUpError.CLOTHE_LIMIT;
                return false;
            }                                    
            error = PopUpError.NO_ERROR;
            return true;            
        }

        Slot emptySlot = slots.Find(x => !x.hasClothe);
        if (emptySlot != null)
        {
            error = PopUpError.NO_ERROR;
            return true;        
        }
        error = PopUpError.INVENTORY_FULL;
        return false;
    }

    public void DisableClothesOfType(ClotheType type)
    {
        for (int i = 0, count = slots.Count; i < count; i++) { 
            if(slots[i].clothe != null && slots[i].clothe.clotheType == type)
            {
                slots[i].isBeingUsed = false;
                slots[i].UpdateSlotColor();
            }
        }
    }

    public bool HasClothe(Clothe clothe)
    {
        Slot slot = slots.Find(x => x.hasClothe && x.clothe.id == clothe.id);
        return slot != null;
    }

    public void UseInventory()
    {
        if (ShopController.instance.isOpen) return;
        if (DialogueManager.instance.isDialogueOn) return;
        if (!isInventoryOpen) OpenInventory();
        else CloseInventory();
    }

    public void UpdatePlayerMoney() => moneyText.text = $"${playerMoney}";

    public void OpenInventory()
    {
        isInventoryOpen = true;
        inventoryPanel.SetActive(true);
        InputController.instance.LockMovement();
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        inventoryPanel.SetActive(false);
        InputController.instance.UnlockMovement();
    }
}
