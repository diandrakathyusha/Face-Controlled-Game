using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public List<GameEvent> gameEvents;   // Event sequence for the game
    private List<bool> eventTriggered;  // Track whether an event has been triggered
    private int currentEventIndex;      // Tracks the current event index

    public ConversationManager dialogueManager;
    public EmailManager emailManager;
    public CallManager callManager;
    public EndingScreenManager endingScreenManager;

    private void Start()
    {
        // Initialize eventTriggered list
        eventTriggered = new List<bool>(new bool[gameEvents.Count]);
    }

    public void TriggerEvent(int currentEventIndex)
    {
        // Validate index
        if (currentEventIndex < 0 || currentEventIndex >= gameEvents.Count) return;

        // Check if the event has already been triggered
        if (eventTriggered[currentEventIndex])
        {
            Debug.LogWarning($"Event ID '{currentEventIndex}' has already been triggered. Skipping.");
            return;
        }

        // Mark the event as triggered
        eventTriggered[currentEventIndex] = true;

        // Get the current event
        GameEvent currentEvent = gameEvents[currentEventIndex];

        // Execute the event based on its type
        switch (currentEvent.eventType)
        {
            case GameEventType.Dialogue:
                dialogueManager.StartDialogue(currentEvent.dialogueData);
                break;
            case GameEventType.Email:
                emailManager.DisplayEmail(currentEvent.emailEntryID);
                break;
            case GameEventType.IncomingCall:
                callManager.IncomingCall(currentEvent.callerName);
                break;
            case GameEventType.EndingScreen:
                endingScreenManager.DisplayEnding(currentEvent.endingID);
                break;
        }

        Debug.Log($"Triggered event ID '{currentEventIndex}'.");
    }
}