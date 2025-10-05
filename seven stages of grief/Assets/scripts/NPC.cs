using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public NPCDialogue dialogueDataKey;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public GameObject Key;

    NPCDialogue currentDialogue;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool canInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        //If no dialogue data or the game is paused and no dialogue is active
        //if(dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
        if(dialogueData == null || (Time.timeScale == 0 && !isDialogueActive))
        return;

        if(isDialogueActive)
        {
            NextLine();

        }
        else
        {
            currentDialogue = dialogueData;

            StartDialogue();
        }
    }

    void StartDialogue()
    {
        if (currentDialogue == null)
        {
            print("no dialogue set");
            return;
        }

        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(currentDialogue.npcName);
        portraitImage.sprite = currentDialogue.npcPortrait;

        dialoguePanel.SetActive(true);
        //To make sure the MC does not move when the NPC is talking
        //PauseController.SetPause(true);
        Time.timeScale = 0f; //pause

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            //Skip typing animation and show the full line
            StopAllCoroutines();
            dialogueText.SetText(currentDialogue.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if(++dialogueIndex < currentDialogue.dialogueLines.Length)
        {
            //If another line, type next line
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        //Typing effect
        foreach(char letter in currentDialogue.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            // yield return new WaitForSeconds(dialogueData.typingSpeed);
            yield return new WaitForSecondsRealtime(currentDialogue.typingSpeed); 
        }

        isTyping = false;

        //Auto Progression
        if(currentDialogue.autoProgressLines.Length >  dialogueIndex && currentDialogue.autoProgressLines[dialogueIndex])
        {
            // yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            yield return new WaitForSecondsRealtime(currentDialogue.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        //PauseController.SetPause(false);
        Time.timeScale = 1f; //resume
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Medicine"))
        {
            Destroy(other.gameObject);
            currentDialogue = dialogueDataKey;
            StartDialogue();
            Key.SetActive(true);

        }
    }
}
