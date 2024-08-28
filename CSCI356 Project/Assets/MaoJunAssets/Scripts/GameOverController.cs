using UnityEngine;
using UnityEngine.UI; // Required for UI components
using System.Collections;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TMP_Text youDieText; // Assign this in the Inspector
    public TMP_Text BossRemainingText;
    public Image WINPopup;
    public TMP_Text Rating;
    public TMP_Text deathText;
    public MapSettingsController MapSettingsController;

    public GameObject enemy1; // Assign this in the Inspector
    public GameObject enemy2; // Assign this in the Inspector
    public GameObject enemy3; // Assign this in the Inspector

    private bool enemy1Dead = false;
    private bool enemy2Dead = false;
    private bool enemy3Dead = false;
    void Start()
    {
        WINPopup.gameObject.SetActive(false);
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

    // Call this method when an enemy dies
    public void OnEnemyDeath(GameObject enemy)
    {
        if (enemy == enemy1)
        {
            enemy1Dead = true;
        }
        else if (enemy == enemy2)
        {
            enemy2Dead = true;
        }
        else if (enemy == enemy3)
        {
            enemy3Dead = true;
        }

        UpdateBossRemainingDisplay();
        CheckAllEnemiesDead();
    }
    public void UpdateBossRemainingDisplay()
    {
        if (BossRemainingText != null)
        {
            int trueCount = CountTrueBooleans(enemy1Dead, enemy2Dead, enemy3Dead);
            // Update the UI text based on the death value
            BossRemainingText.text = $"BossRemaining: {3-trueCount}";
        }
    }
    // Check if all enemies are dead
    private void CheckAllEnemiesDead()
    {
        if (enemy1Dead && enemy2Dead && enemy3Dead)
        {
            Debug.Log("All enemies are dead. You win!");
            WINPopup.gameObject.SetActive(true);

            // Update the UI text based on the death value
            deathText.text = $"Death: {MapSettingsController.death}";
            if (MapSettingsController.death == 0)
            {
                Rating.text = "S";
            }else if (MapSettingsController.death <= 2) { Rating.text = "A"; }
            else if (MapSettingsController.death <= 4) { Rating.text = "B"; }
            else { Rating.text = "C"; }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    int CountTrueBooleans(params bool[] booleans)
    {
        int count = 0;
        foreach (bool b in booleans)
        {
            if (b)
            {
                count++;
            }
        }
        return count;
    }
}