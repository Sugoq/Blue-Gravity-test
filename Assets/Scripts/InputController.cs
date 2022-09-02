using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    public static InputController instance;


    [SerializeField] KeyCode interactKey;
    public Vector2 movement {get; private set;}
    [HideInInspector] public Action onInteract;

    private void Awake() => instance = this;

    private void OnDestroy() => instance = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            
        }
    }
}
