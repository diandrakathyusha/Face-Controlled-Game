using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public List<string> responseTextSequence;
    public string requiredEmotion;
    public bool autoAdvance;
    public DialogueDatas nextNode;
    public string resultingDialogueID;
    public int resultingEmailID; // Link to resulting email
}

[CreateAssetMenu(fileName = "DialogueDatas", menuName = "ScriptableObjects/DialogueDatas")]
public class DialogueDatas : ScriptableObject
{
    public string dialogueID;
    public List<string> npcTextSequence;
    public List<AudioClip> npcVoiceoverClips;  // Add this to store voiceover audio for each line
    public List<DialogueOption> options;
}
