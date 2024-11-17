using MoodMe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVideoCall : MonoBehaviour
{
    public EmotionsManager emotions;
    public Slider timeSlider;
    public Image playerImage;
    public Sprite sad, neutral, surprised;
    public Button submitButton;
    public ConversationManager conversationManager;
    public EventManager eventManager;

    private float timer;
    private string currentEmotion;

    private void Start()
    {
        timer = 15f;
        timeSlider.maxValue = timer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        timeSlider.value = timer;

        if (timer > 0)
        {
            DetectEmotion();
        }

        if (timer <= 0 && submitButton.interactable)
        {
            SubmitEmotionChoice();
        }
    }

    private void DetectEmotion()
    {
        if (emotions.Neutral >= 0.5f)
        {
            playerImage.sprite = neutral;
            currentEmotion = "neutral";
        }
        else if (emotions.Sad >= 0.05f)
        {
            playerImage.sprite = sad;
            currentEmotion = "sad";
        }
        else if (emotions.Surprised >= 0.5f)
        {
            playerImage.sprite = surprised;
            currentEmotion = "surprised";
        }

        conversationManager.DisplayCurrentDialogue(currentEmotion); // Show player's response based on emotion

    }

    private void SubmitEmotionChoice()
    {
        if (conversationManager != null)
        {
            conversationManager.DisplayDialogue(currentEmotion); // Show player's response based on emotion
            Debug.Log("Submit emotion");
        }

        //submitButton.interactable = false;
        timer = 15f; // Reset timer for next emotion selection
    }

    public void EndVideoCall()
    {
        gameObject.SetActive(false); // Assumes the video call window is the GameObject this script is attached to
        Debug.Log("Video call ended.");
        eventManager.TriggerEvent();
    }
}
