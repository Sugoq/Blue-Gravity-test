using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ClotheController : SingletonMonoBehaviour<ClotheController>
{
    public List<Clothe> clothes;
    public SpriteRenderer spriteRenderer;
    public Color skinColor;

    private void Start()
    {
        UpdateClothes();
    }

    public void AddClothe(Clothe clothe)
    {
        int index = clothes.FindIndex(x => x.clotheType == clothe.clotheType);
        if (index >= 0) clothes.RemoveAt(index);
        clothes.Add(clothe);
        UpdateClothes();
        
    }

    public void UpdateClothes()
    {
        Color red, blue, green, yellow, purple, cyan, white;
        red = blue = green = yellow = purple = cyan = white = skinColor;
        foreach(Clothe clothe in clothes)
        {
            if (clothe.redReplacer.a > 0.5f) red = clothe.redReplacer; 
            if (clothe.blueReplacer.a > 0.5f) blue = clothe.blueReplacer; 
            if (clothe.greenReplacer.a > 0.5f) green = clothe.greenReplacer; 
            if (clothe.yellowReplacer.a > 0.5f) yellow = clothe.yellowReplacer; 
            if (clothe.purpleReplacer.a > 0.5f) purple = clothe.purpleReplacer; 
            if (clothe.cyanReplacer.a > 0.5f) cyan = clothe.cyanReplacer; 
            if (clothe.whiteReplacer.a > 0.5f) white = clothe.whiteReplacer; 
        }
        Material material = spriteRenderer.material;
        material.SetColor("_RedReplacer", red);
        material.SetColor("_BlueReplacer", blue);
        material.SetColor("_GreenReplacer", green);
        material.SetColor("_YellowReplacer", yellow);
        material.SetColor("_PurpleReplacer", purple);
        material.SetColor("_CyanReplacer", cyan);
        material.SetColor("_WhiteReplacer", white);    
    }

    public void RemoveClothe(Clothe clothe)
    {
        int index = clothes.FindIndex(x => x.title == clothe.title);
        if (index >= 0) clothes.RemoveAt(index);
        UpdateClothes();
    }


}
