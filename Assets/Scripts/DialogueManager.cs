using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class DialogueManager : SingletonMonoBehaviour<DialogueManager>
{
    public bool isDialogueOn;
    private bool isClosingDialogue;
    private bool canDialogue;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private float timeToCloseDialogue = 0.5f;
    private DialogueSequence currentDialogue;
    private int dialogueIndex;

    private void Start()
    {
        ShopController.instance.onOpenShop += OnOpenShop;
        ShopController.instance.onCloseShop += OnCloseShop;
        canDialogue = true;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        ShopController.instance.onOpenShop -= OnOpenShop;
        ShopController.instance.onCloseShop -= OnCloseShop;
    }

    public void StartDialogue(DialogueSequence sequence)
    {
        if (isClosingDialogue || !canDialogue) return;
        if (isDialogueOn)
        {
            NextDialogue();
            return;
        }
        dialogueIndex = 0;
        currentDialogue = sequence;

        dialoguePanel.SetActive(true);
        isDialogueOn = true;
        
        CallDisplayDialogue(currentDialogue.dialogues[dialogueIndex++].dialogue);
    }

    public void NextDialogue()
    {
        if (!isDialogueOn) return;

        if (currentDialogue.dialogues.Count > dialogueIndex)
        {
            CallDisplayDialogue(currentDialogue.dialogues[dialogueIndex++].dialogue);
        }
        else
        {
            currentDialogue.onDialogueEnd?.Invoke();
        }
    }

    public void EndDialogueText()        
    {
        if (!isDialogueOn) return;
        CallDisplayDialogue(currentDialogue.dialogues[dialogueIndex].endDialogue);
        isClosingDialogue = true;
    }

    public void CloseDialogue()
    {
        isDialogueOn = false;
        isClosingDialogue = false;
        InputController.instance.UnlockMovement();
        dialoguePanel.SetActive(false);
    }

    public void OnOpenShop()
    {
        isDialogueOn = false;
        isClosingDialogue = false;
        dialoguePanel.SetActive(false);
        canDialogue = false;
    }

    public void OnCloseShop() => canDialogue = true;

    IEnumerator DisplayDialogueRoutine(string dialogue)
    {
        foreach (char c in dialogue)
        {
            dialogueText.text += c;
            yield return new WaitForFixedUpdate();
        }
    }
  
    private void CallDisplayDialogue(string dialogue)
    {
        dialogueText.text = "";
        StopAllCoroutines();
        StartCoroutine(DisplayDialogueRoutine(dialogue));
    }
}
