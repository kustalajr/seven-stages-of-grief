using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

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
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;

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
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if(++dialogueIndex < dialogueData.dialogueLines.Length)
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
        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            // yield return new WaitForSeconds(dialogueData.typingSpeed);
            yield return new WaitForSecondsRealtime(dialogueData.typingSpeed); 
        }

        isTyping = false;

        //Auto Progression
        if(dialogueData.autoProgressLines.Length >  dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            // yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            yield return new WaitForSecondsRealtime(dialogueData.autoProgressDelay);
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
}
