using UnityEngine;
using System.Collections.Generic;

public enum GameEventType
{
    Dialogue,
    Email,
    IncomingCall,
    EndingScreen
}

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    public GameEventType eventType;
    public DialogueDatas dialogueData;  // Optional, only for Dialogue events
    public int emailEntryID;  
    public string callerName;           // Optional, for Incoming Call events
    public int endingID;           // Optional, only for Ending Screen events
}
