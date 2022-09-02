using UnityEngine;

public class Test : InteractableMonoBehaviour
{
    public Clothe clothe;
    protected override void Interact()
    {
        ClotheController.instance.AddClothe(clothe);
    }

    void Update()
    {
        
    }
}
