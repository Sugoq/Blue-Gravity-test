using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string name;
    
    [TextArea(0,2)]
    public string dialogue;
    public string endDialogue;  
}
