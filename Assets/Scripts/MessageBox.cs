using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MessageBox : MonoBehaviour
{
    public GameObject messageBox; // The panel acting as the message box
    public TextMeshProUGUI messageText; // The text component to display the message
    public float displayDuration = 3f; // Duration to display the message box

    private Coroutine hideCoroutine;

    void Start()
    {
        // Ensure the message box is hidden at the start
        messageBox.SetActive(false);
    }

    // Method to show the message box with a specific message
    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageBox.SetActive(true);

        // Stop the previous coroutine if it is still running
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        // Start a new coroutine to hide the message box after a delay
        hideCoroutine = StartCoroutine(HideMessageAfterDelay(displayDuration));
    }

    // Coroutine to hide the message box after a delay
    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageBox.SetActive(false);
    }
}
