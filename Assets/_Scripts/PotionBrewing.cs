using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionBrewing : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText; // Serialized field to assign the TMP Text component in the Inspector

    public Button brewButton;

    private int score = 0;
    private int activePotionLevel = 1;

    public delegate void ScoreUpdated(int newScore);
    public static event ScoreUpdated OnScoreUpdated;

    private void Start()
    {
        // Load the previous score and active potion level from PlayerPrefs (if available)
        score = PlayerPrefs.GetInt("PotionScore", 0);
        activePotionLevel = PlayerPrefs.GetInt("ActivePotionLevel", 1);

        // Add a listener to the brew button's click event
        brewButton.onClick.AddListener(BrewPotion);

        // Initial UI update
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Raise an event to update the score in the UI
        OnScoreUpdated?.Invoke(score);

        // Update the TMP Text component with the new score
        scoreText.text = "Score: " + score.ToString();
    }

    private void BrewPotion()
    {
        int potionValue = GetPotionValue(activePotionLevel);
        score += potionValue;

        // Save the score
        PlayerPrefs.SetInt("PotionScore", score);
        PlayerPrefs.Save();

        // Update the UI
        UpdateUI();
    }
    private int GetPotionValue(int level)
    {
        switch (level)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 4;
            case 4:
                return 8;
            case 5:
                return 16;
            case 6:
                return 32;
            case 7:
                return 64;
            case 8:
                return 128;
            case 9:
                return 256;
            default:
                return 0;
        }
    }
}
