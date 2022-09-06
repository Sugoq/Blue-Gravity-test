using UnityEngine;
using System.Collections.Generic;

public class ClotheController : SingletonMonoBehaviour<ClotheController>
{
    [HideInInspector] public List<Clothe> clothes;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer previewRenderer;
    public Color skinColor;

    public void LoadClothes(List<Clothe> clothes)
    {
        this.clothes = clothes;
        UpdateClothes();
    }

    public string GetEquipString()
    {
        string s = string.Empty;
        for (int i = 0; i < clothes.Count; i++)
        {
            s += $"{clothes[i].id}";

            if (i < clothes.Count - 1)
                s += "-";
        }
        return s;
    }

    public void AddClothe(Clothe clothe)
    {
        int index = clothes.FindIndex(x => x.clotheType == clothe.clotheType);
        if (index >= 0)
        {
            clothes.RemoveAt(index);
        }
        clothes.Add(clothe);
        UpdateClothes();
        MemoryController.instance.SaveEquippeds();
    }

    public void UpdateClothes()
    {
        Color red, blue, green, yellow, purple, cyan, white;
        red = blue = green = yellow = purple = cyan = white = skinColor;
        foreach(Clothe clothe in clothes)
        {
            if (clothe.t_shirt.a > 0.5f) red = clothe.t_shirt; 
            if (clothe.blueReplacer.a > 0.5f) blue = clothe.blueReplacer; 
            if (clothe.pants.a > 0.5f) green = clothe.pants; 
            if (clothe.shirt.a > 0.5f) yellow = clothe.shirt; 
            if (clothe.underShirt.a > 0.5f) purple = clothe.underShirt; 
            if (clothe.shoes.a > 0.5f) cyan = clothe.shoes; 
            if (clothe.skin.a > 0.5f) white = clothe.skin; 
        }
        Material material = spriteRenderer.material;
        material.SetColor("_RedReplacer", red);
        material.SetColor("_BlueReplacer", blue);
        material.SetColor("_GreenReplacer", green);
        material.SetColor("_YellowReplacer", yellow);
        material.SetColor("_PurpleReplacer", purple);
        material.SetColor("_CyanReplacer", cyan);
        material.SetColor("_WhiteReplacer", white);
        PreviewController.instance.UpdatePreview(null);
    }

    public void RemoveClothe(Clothe clothe)
    {
        int index = clothes.FindIndex(x => x.title == clothe.title);
        if (index >= 0)
        {
            clothes.RemoveAt(index);
        }
        UpdateClothes();
        MemoryController.instance.SaveEquippeds();
    }


}
