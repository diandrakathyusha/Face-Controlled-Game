using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailManager : MonoBehaviour
{
    public AudioSource notificationSound;
    public Image notificationIcon;
    public GameObject notificationWindow;
    public SceneObjectManager sceneObjectManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayEmail(string emailEntryID)
    {
        var emailEntry = sceneObjectManager.GetObjectByID(emailEntryID);
        if (emailEntry != null)
        {
            notificationSound.Play();
            notificationIcon.gameObject.SetActive(true);
            StartCoroutine(DisplayNotification());
            emailEntry.SetActive(true);  // Activate the email GameObject
        }
        else
        {
            Debug.LogWarning($"Email entry with ID '{emailEntryID}' not found.");
        }
    }

    IEnumerator DisplayNotification()
    {
        notificationWindow.SetActive(true);
        yield return new WaitForSeconds(5);
        notificationWindow.SetActive(false);
    }
}
