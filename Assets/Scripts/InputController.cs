using System;
using UnityEngine;

public class InputController : SingletonMonoBehaviour<InputController>
{
    [SerializeField] KeyCode interactKey;

    public Vector2 movement {get; private set;}
    [HideInInspector] public Action onInteract;
    
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(interactKey))
        {
            onInteract?.Invoke();        
        }
    }
}
