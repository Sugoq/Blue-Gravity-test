using System;
using UnityEngine;

public class InputController : SingletonMonoBehaviour<InputController>
{
    private bool lockMovementOnInteraction;
    public Vector2 movement {get; private set;}
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private KeyCode stopInteractKey;
    [SerializeField] private KeyCode inventoryKey;

     public Action onInteract;
     public Action stopInteract;

    void Update()
    {
        if (!lockMovementOnInteraction) movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else movement = Vector2.zero;
        if (Input.GetKeyDown(interactKey))
        {
            onInteract?.Invoke();        
        }
        if (Input.GetKeyDown(stopInteractKey))
        {
            stopInteract?.Invoke();
        }
        if (Input.GetKeyDown(inventoryKey))
        {
            InventoryController.instance.UseInventory();
        }
    }

    public void LockMovement() => lockMovementOnInteraction = true;

    public void UnlockMovement() => lockMovementOnInteraction = false;
}
