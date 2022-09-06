using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothe Asset", menuName = "ScriptableObjects/Clothe Asset", order = 1)]
public class Clothe : ScriptableObject
{
    [Header("Item Info")]
    public int id;
    public string title;
    public int price;
    public Sprite sprite;
    public ClotheType clotheType;    
    
    [Header("Shader Info")]
    [Header("Set alpha to 0 if the color is not used")]
    public Color t_shirt;
    public Color pants;
    [HideInInspector]public Color blueReplacer;
    public Color shirt;
    public Color underShirt;
    public Color shoes;
    public Color skin;
}

public enum ClotheType
{
    SHIRT, PANTS, SHOES
}

