using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreenManager : MonoBehaviour
{
    public AudioSource notificationSound;
    public SceneObjectManager sceneObjectManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayEnding(int endingID)
    {
        var endingWindow = sceneObjectManager.GetObjectByID(endingID);
        if (endingWindow != null)
        {
            notificationSound.Play();
            endingWindow.SetActive(true);  // Activate the email GameObject
        }
        else
        {
            Debug.LogWarning($"Ending with ID '{endingID}' not found.");
        }
    }

}
