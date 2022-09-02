using UnityEngine;

public class InteractableMonoBehaviour : MonoBehaviour
{
    public float distanceToInteract;
    protected virtual void Start()
    {
        InputController.instance.onInteract += TryInteraction;     
    }

    protected virtual void OnDestroy()
    {
        InputController.instance.onInteract -= TryInteraction;
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    protected void TryInteraction()
    {
        if(Vector2.Distance((Vector2)transform.position, (Vector2)PlayerMovement.instance.transform.position) <= distanceToInteract)
        {
            Interact();    
        }
    }

    protected virtual void Interact()
    {

    }


}
