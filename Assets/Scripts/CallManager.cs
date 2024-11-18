using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CallManager : MonoBehaviour
{
    public Slider loadingSlider;
    public GameObject videoCallWindow;
    public GameObject incomingCallWindow;
    public GameObject infoWindow;
    public AudioSource waitTone, ringTone;
    public EventManager eventManager;
    public float loadTime = 4f;  // Duration of the loading slider timer

    private void Start()
    {
        // Ensure all windows are deactivated at start
        loadingSlider.gameObject.SetActive(false);
        videoCallWindow.SetActive(false);
        infoWindow.SetActive(false);
    }

    // Call function: Activates loading slider, then video call window after timer
    public void Call()
    {
        StartCoroutine(CallRoutine());
    }

    private IEnumerator CallRoutine()
    {
        waitTone.Play();
        loadingSlider.gameObject.SetActive(true);
        loadingSlider.maxValue = loadTime;
        loadingSlider.value = 0;

        // Increment slider progress over the timer
        float elapsedTime = 0f;
        while (elapsedTime < loadTime)
        {
            elapsedTime += Time.deltaTime;
            loadingSlider.value = Mathf.Lerp(0, loadTime, elapsedTime / loadTime);
            yield return null;
        }

        loadingSlider.gameObject.SetActive(false);
        videoCallWindow.SetActive(true);
    }

    public void IncomingCall(string callerName)
    {
        incomingCallWindow.SetActive(true);
        ringTone.Play();
    }
    // Accept call function: Immediately activates video call window
    public void AcceptCall()
    {
        StopAllCoroutines(); // Stop any ongoing coroutines
        loadingSlider.gameObject.SetActive(false);
        videoCallWindow.SetActive(true);
    }

    public void RejectCall()
    {
        StartCoroutine(Callback());
    }

    private IEnumerator Callback()
    {
        yield return new WaitForSeconds(3f);
        IncomingCall("Zoe");
    }

 // Call not picked up: Shows slider, then "call not picked up" info window
        public void CallNotPickedUp()
    {
        StartCoroutine(CallNotPickedUpRoutine());
    }

    private IEnumerator CallNotPickedUpRoutine()
    {
        waitTone.Play();
        loadingSlider.gameObject.SetActive(true);
        loadingSlider.maxValue = loadTime;
        loadingSlider.value = 0;

        float elapsedTime = 0f;
        while (elapsedTime < loadTime)
        {
            elapsedTime += Time.deltaTime;
            loadingSlider.value = Mathf.Lerp(0, loadTime, elapsedTime / loadTime);
            yield return null;
        }

        loadingSlider.gameObject.SetActive(false);
        infoWindow.SetActive(true);  // Show "busy" info after timer
    }

    // Reset all windows (e.g., at the end of a call)
    public void ResetCallWindows()
    {
        loadingSlider.gameObject.SetActive(false);
        videoCallWindow.SetActive(false);
        infoWindow.SetActive(false);
    }
}
