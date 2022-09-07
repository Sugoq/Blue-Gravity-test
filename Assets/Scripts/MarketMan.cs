using UnityEngine;
public class MarketMan : InteractableMonoBehaviour
{
    [SerializeField] private DialogueSequence dialogueSequence;    
    private bool isShopOpen;
    private bool isClosingDialogue;

    protected override void Start()
    {
        base.Start();
        ShopController.instance.onOpenShop += OnShop;
        ShopController.instance.onCloseShop += OnShop;
    
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        ShopController.instance.onOpenShop -= OnShop;
        ShopController.instance.onCloseShop -= OnShop;
    }

    protected override void Interact()
    {
        base.Interact();
        if (isShopOpen) return;
        dialogueSequence.TriggerDialogue();
        InputController.instance.LockMovement();
    }

    protected override void StopInteraction()
    {
        base.StopInteraction();
        if (!isClosingDialogue)
        {
            DialogueManager.instance.EndDialogueText();
            isClosingDialogue = true;
            return;
        }
        if (isClosingDialogue)
        {
            DialogueManager.instance.CloseDialogue();
            isClosingDialogue = false;
        }
    }

    public void OnShop()
    {
        isShopOpen = !isShopOpen;
        if (!isShopOpen)
        {
            InputController.instance.UnlockMovement();
            isInteracting = false;
        }
    }
}
