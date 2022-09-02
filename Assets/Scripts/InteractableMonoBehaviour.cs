using UnityEngine;
using Conditional = System.Diagnostics.ConditionalAttribute;

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


    [Conditional("UNITY_EDITOR")]
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distanceToInteract);    
    }

    protected void TryInteraction()
    {
        if(Vector2.Distance(transform.position, PlayerMovement.instance.transform.position) <= distanceToInteract)
        {
            Interact();    
        }
    }

    protected virtual void Interact()
    {

    }
}
