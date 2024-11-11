using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIPopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RectTransform uiPanel;  // Assign the UI panel RectTransform in the Inspector
    public float popupDuration = 0.5f;  // Duration of the popup animation
    public float holdDuration = 3f;     // Duration to hold the popup on screen

    private Vector2 startPosition;
    public Vector2 targetPosition;

    private void Start()
    {
        // Set the starting and target positions
        startPosition = new Vector2(0, Screen.height); // Start above the screen

        // Hide the panel offscreen initially
        uiPanel.anchoredPosition = startPosition;

        // Add listener to your event
        TextInput.onTextChange += ShowPopup;
    }

    private void OnDestroy()
    {
        // Remove the listener to prevent memory leaks
        TextInput.onTextChange -= ShowPopup;
    }

    private void ShowPopup(string item)
    {
        text.text = "Product " + item + "Changed";
        // Start the popup animation coroutine
        StartCoroutine(AnimatePopup());
    }

    private IEnumerator AnimatePopup()
    {
        // Move the UI panel down to the center
        LeanTween.move(uiPanel, targetPosition, popupDuration).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(popupDuration + holdDuration);

        // Move the UI panel back up to hide it
        LeanTween.move(uiPanel, startPosition, popupDuration).setEase(LeanTweenType.easeInBack);
    }
}
