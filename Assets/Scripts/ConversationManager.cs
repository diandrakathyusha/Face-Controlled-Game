using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConversationManager : MonoBehaviour
{
    public PlayerVideoCall playerVideoCall;
    public Text npcDialogueText;
    public Text playerResponseText;
    public Button continueButton;
    public DialogueDatas startingNode;
    public DialogueDatas currentNode;
    public AudioSource audioSource;
    public GameObject videoCallWindow;


    private int sentenceIndex = 0;                 // Tracks current sentence in NPC dialogue sequence
    private bool awaitingPlayerResponse = false;   // Indicates if awaiting a player response

    private void Start()
    {
        continueButton.onClick.AddListener(ContinueDialogue);
    }

    public void StartDialogue(DialogueDatas Node)
    {
        currentNode = Node;
        SetStartingNode(currentNode);
        //videoCallWindow.SetActive(true); // Activate the video call window when dialogue starts
        // DisplayNPCText();
    }

    public void DisplayDialogue(string playerEmotion)
    {
        if (currentNode == null) return;

        playerResponseText.text = ""; // Clear previous player response
        awaitingPlayerResponse = false;
        sentenceIndex = 0; // Reset to the first sentence
        continueButton.gameObject.SetActive(true); // Show the continue button

        DialogueOption chosenOption = null;

        // Determine if there’s an auto-advance option or one matching player emotion
        foreach (var option in currentNode.options)
        {
            if (option.autoAdvance || option.requiredEmotion == playerEmotion)
            {
                chosenOption = option;
                break;
            }
        }

        if (chosenOption != null)
        {
            if (chosenOption.autoAdvance)
            {
                // Auto-advance to the next NPC dialogue
                currentNode = chosenOption.nextNode;
                DisplayNPCText();
                return; // Skip player response and reset
            }
            else
            {
                // Show player response in sequence and prepare to load next node
                StartCoroutine(DisplayPlayerResponseAndNPCDialogue(chosenOption.responseTextSequence));
                currentNode = chosenOption.nextNode;
            }
        }
        else
        {
            Debug.LogWarning("No matching dialogue option found for emotion: " + playerEmotion);
            EndDialogue();
        }
    }

    // Coroutine to display player's response and then continue to NPC dialogue
    private System.Collections.IEnumerator DisplayPlayerResponseAndNPCDialogue(List<string> responseTextSequence)
    {
        awaitingPlayerResponse = true;
        foreach (string response in responseTextSequence)
        {
            playerResponseText.text = response;
            yield return new WaitForSeconds(2f); // Adjust delay as needed
        }
        awaitingPlayerResponse = false;

        // Display the next NPC dialogue after player's response
        DisplayNPCText();
    }

    public void ContinueDialogue()
    {
        if (currentNode == null) return;

        if (!awaitingPlayerResponse && sentenceIndex < currentNode.npcTextSequence.Count)
        {
            npcDialogueText.text = currentNode.npcTextSequence[sentenceIndex];

            // Stop current audio and play corresponding voiceover clip
            if (audioSource.isPlaying) audioSource.Stop();
            if (sentenceIndex < currentNode.npcVoiceoverClips.Count && currentNode.npcVoiceoverClips[sentenceIndex] != null)
            {
                audioSource.clip = currentNode.npcVoiceoverClips[sentenceIndex];
                audioSource.Play();
            }

            sentenceIndex++;

            if (sentenceIndex >= currentNode.npcTextSequence.Count)
            {
                awaitingPlayerResponse = true;
                CheckAutoAdvance();
            }
        }
        else if (awaitingPlayerResponse)
        {
            npcDialogueText.text = ""; // Clear NPC text to show player’s response

            // If no more responses or nodes, end the conversation
            if (currentNode == null || currentNode.options.Count == 0)
            {
                EndDialogue();
            }
        }
    }


    private void CheckAutoAdvance()
    {
        foreach (var option in currentNode.options)
        {
            if (option.autoAdvance)
            {
                // Directly move to the next node if auto-advance is enabled
                currentNode = option.nextNode;
                DisplayNPCText();
                return;
            }
        }
    }

    public void SetStartingNode(DialogueDatas startingNode)
    {
        DisplayNPCText();
    }

    private void DisplayNPCText()
    {
        sentenceIndex = 0;
        awaitingPlayerResponse = false;
        ContinueDialogue(); // Display the first sentence immediately
    }


    public void DisplayCurrentDialogue(string playerEmotion)
    {
        if (currentNode == null) return;

        playerResponseText.text = ""; // Clear previous player response
        continueButton.gameObject.SetActive(false); // Hide continue button until player responds

        // Find the correct dialogue option based on emotion
        DialogueOption chosenOption = currentNode.options.Find(option => option.requiredEmotion == playerEmotion);

        if (chosenOption != null)
        {
            StartCoroutine(DisplayPlayerResponseAndNPCDialogue(chosenOption.responseTextSequence));

        }
    }

    private void EndDialogue()
    {
        Debug.Log("Dialogue ended.");
        playerVideoCall.EndVideoCall(); // Call the method to deactivate the video call window
    }

}
