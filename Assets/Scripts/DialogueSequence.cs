using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSequence : MonoBehaviour
{
    public List<Dialogue> dialogues;
    public UnityEvent onDialogueEnd;

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(this);
    }
}
