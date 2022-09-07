using UnityEngine;
using Conditional = System.Diagnostics.ConditionalAttribute;

public class InteractableMonoBehaviour : MonoBehaviour
{
    [HideInInspector] public bool isInteracting;
    [SerializeField] private GameObject interactionIndicator;
    [SerializeField] private Transform indicatorPosition;
    private GameObject currentIndicator;
    public float distanceToInteract;
    protected virtual void Start()
    {
        InputController.instance.onInteract += TryInteraction;     
        InputController.instance.stopInteract += TryToStopInteraction;     
    }

    protected virtual void OnDestroy()
    {
        InputController.instance.onInteract -= TryInteraction;
        InputController.instance.stopInteract -= TryToStopInteraction;
    }

    private void Update()
    {
        if(IsClose())
        {
            if(!isInteracting && currentIndicator == null && interactionIndicator != null)
                currentIndicator = Instantiate(interactionIndicator, indicatorPosition.position, Quaternion.identity, indicatorPosition);
        }
        else
        {
            if (currentIndicator != null) Destroy(currentIndicator);
        }


    }

    [Conditional("UNITY_EDITOR")]
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distanceToInteract);    
    }
    
    public bool IsClose() => Vector2.Distance(transform.position, Player.instance.transform.position) <= distanceToInteract;

    protected void TryInteraction()
    {
        if(IsClose())
            Interact();               
    }

    protected void TryToStopInteraction()
    {               
        StopInteraction();       
    }
    
    protected virtual void StopInteraction()
    {
        isInteracting = false;
    }

    protected virtual void Interact()
    {
        isInteracting = true;
        if (currentIndicator != null) Destroy(currentIndicator);
    }
}
