using UnityEngine;
using UnityEngine.UI; // Required for UI components
using System.Collections;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TMP_Text youDieText; // Assign this in the Inspector

    void Start()
    {
        youDieText.enabled = false; // Ensure the text is initially hidden
    }

    public void ShowYouDieText()
    {
        StartCoroutine(ShowTextCoroutine());
    }

    private IEnumerator ShowTextCoroutine()
    {
        youDieText.enabled = true; // Show the text
        yield return new WaitForSeconds(1f); // Wait for 1 second
        youDieText.enabled = false; // Hide the text
    }
}