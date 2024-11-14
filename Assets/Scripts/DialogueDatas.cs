using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public List<string> responseTextSequence;
    public string requiredEmotion;
    public bool autoAdvance;
    public DialogueDatas nextNode;
}

[CreateAssetMenu(fileName = "DialogueDatas", menuName = "ScriptableObjects/DialogueDatas")]
public class DialogueDatas : ScriptableObject
{
    public List<string> npcTextSequence;
    public List<AudioClip> npcVoiceoverClips;  // Add this to store voiceover audio for each line
    public List<DialogueOption> options;
}
