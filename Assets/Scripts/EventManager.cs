using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public List<GameEvent> gameEvents;   // Event sequence for the game
    private int currentEventIndex = 0;

    public ConversationManager dialogueManager;
    public EmailManager emailManager;
    public CallManager callManager;
//    public EndingScreenManager endingScreenManager;

    private void Start()
    {

    }

    public void TriggerEvent()
    {
        if (currentEventIndex >= gameEvents.Count) return;

        GameEvent currentEvent = gameEvents[currentEventIndex];
        switch (currentEvent.eventType)
        {
            //when making a call
            case GameEventType.Dialogue:
                dialogueManager.StartDialogue(currentEvent.dialogueData);
                break;
            case GameEventType.Email:
                emailManager.DisplayEmail(currentEvent.emailEntry);
                break;
            //when receiving a call
            case GameEventType.IncomingCall:
//                callManager.StartCall(currentEvent.callerName);
                break;
            case GameEventType.EndingScreen:
//                endingScreenManager.ShowEnding(currentEvent.endingText);
                break;
        }

        currentEventIndex++;
    }

    // Call this when an event ends to trigger the next one
    public void OnEventComplete()
    {
        TriggerEvent();
    }


}
