using UnityEngine;
using System.Collections.Generic;

public class MemoryController : SingletonMonoBehaviour<MemoryController>
{
    private const string inventoryKey = "Inventory";
    private const string equipKey = "Equip";
    [SerializeField] private List<Clothe> clothes;
   
    void Start()
    {
        LoadEquip();
        LoadInventory();
        ClotheController.instance.UpdateClothes();
        PreviewController.instance.UpdatePreview(null);
    }

    public void SaveInventory()
    {
        string s = InventoryController.instance.GetInventoryString();
        print($"Saving Inventory {s}");
        PlayerPrefs.SetString(inventoryKey, s);
    }


    public void SaveEquippeds()
    {
        string s = ClotheController.instance.GetEquipString();
        print($"Saving Equip {s}");
        PlayerPrefs.SetString(equipKey, s);
    }

    public void LoadInventory()
    {
        string s = PlayerPrefs.GetString(inventoryKey, InventoryController.instance.GetInventoryString());
        InventoryController.instance.LoadSlots(ExtractIntegers(s));
    }

    public void LoadEquip()
    {
        List<Clothe> currentClothes = new List<Clothe>();
        string s = PlayerPrefs.GetString(equipKey, ClotheController.instance.GetEquipString());
        print($"Loading Equip {s}");
        var ids = (ExtractIntegers(s));
        foreach (int id in ids) currentClothes.Add(clothes.Find(x => x.id == id));

        ClotheController.instance.LoadClothes(currentClothes);
    }

    public Clothe GetClotheById(int id) => clothes.Find(x => x.id == id);
    private List<int> ExtractIntegers(string text)
    {
        List<int> list = new List<int>();
        string integerString = string.Empty;
        foreach (char c in text)
        {
            if (char.IsDigit(c))
            {
                integerString += c;
            }
            else if (integerString.Length > 0)
            {
                list.Add(int.Parse(integerString));
                integerString = "";
            }
        }
        if(integerString.Length > 0) list.Add(int.Parse(integerString));
        return list;
    }
}
