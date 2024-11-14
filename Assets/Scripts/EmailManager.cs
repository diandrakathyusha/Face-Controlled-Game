using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailManager : MonoBehaviour
{
    public AudioSource notificationSound;
    public Image notificationIcon;
    public GameObject notificationWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayEmail(GameObject emailEntry)
    {
        if (emailEntry != null)
        {
            notificationSound.Play();
            notificationIcon.gameObject.SetActive(true);
            StartCoroutine(DisplayNotification());
            emailEntry.SetActive(true);  // Display the email entry GameObject
        }
        else
        {
            Debug.LogWarning("Email entry GameObject is not assigned in the GameEvent.");
        }
    }


    IEnumerator DisplayNotification()
    {
        notificationWindow.SetActive(true);
        yield return new WaitForSeconds(5);
        notificationWindow.SetActive(false);
    }
}
