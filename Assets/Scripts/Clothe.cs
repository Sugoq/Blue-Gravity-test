using UnityEngine;

[CreateAssetMenu(fileName = "Clothe Asset", menuName = "ScriptableObjects/Clothe Asset", order = 1)]
public class Clothe : ScriptableObject
{
    [Header("Item Info")]
    public string title;
    public int price;
    public Sprite sprite;
    
    [Header("Shader Info")]
    public Color redReplacer;
    public Color greenReplacer;
    public Color blueReplacer;
    public Color yellowReplacer;
    public Color purpleReplacer;
    public Color cyanReplacer;
    public Color whiteReplacer;
}

