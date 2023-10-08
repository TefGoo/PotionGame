using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerGame : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text potionNameText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text scoreText;

    [Header("Potion Buttons")]
    [SerializeField] private Button[] potionButtons;

    [Header("Potion Data")]
    [SerializeField] private int[] multipliers = { 1, 2, 4, 8, 16, 32, 64, 128, 256 };
    [SerializeField] private int[] unlockCosts = { 0, 20, 130, 400, 1000, 2500, 6000, 15000, 40000 };

    [Header("Click Button")]
    [SerializeField] private Button clickButton;

    [Header("Level Up Button")]
    [SerializeField] private Button levelUpButton; // Reference to the "Level Up" button

    [Header("Level Buttons")]
    [SerializeField] private Button[] levelButtons; // Reference to the buttons for levels 2 to 9

    private int score = 0;
    private int activePotionLevel = 1;

    private void Start()
    {
        score = PlayerPrefs.GetInt("PotionScore", 0);
        activePotionLevel = PlayerPrefs.GetInt("ActivePotionLevel", 1);

        for (int i = 0; i < potionButtons.Length; i++)
        {
            int potionLevel = i + 1;
            Button button = potionButtons[i];

            button.onClick.AddListener(() =>
            {
                OnPotionButtonClick(potionLevel);
            });
        }

        clickButton.onClick.AddListener(Click);

        // Attach click listeners to level buttons
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelToUnlock = i + 2; // Levels 2 to 9
            Button levelButton = levelButtons[i];

            levelButton.onClick.AddListener(() =>
            {
                OnLevelButtonClick(levelToUnlock);
            });
        }

        // Attach click listener to the "Level Up" button
        levelUpButton.onClick.AddListener(LevelUp);

        UpdateUI();
    }

    private void Update()
    {
        // Check if the player has enough points to unlock the next potion level
        if (activePotionLevel < multipliers.Length && score >= unlockCosts[activePotionLevel])
        {
            // Enable the "Level Up" button
            levelUpButton.gameObject.SetActive(true);
        }
        else
        {
            // Disable the "Level Up" button if the player doesn't have enough points
            levelUpButton.gameObject.SetActive(false);
        }
    }

    public void Click()
    {
        score += multipliers[activePotionLevel - 1];
        UpdateUI();
    }

    private void UpdateUI()
    {
        potionNameText.text = "Potion Level " + activePotionLevel;
        costText.text = "Cost: " + unlockCosts[activePotionLevel];
        levelText.text = "Level: " + activePotionLevel;
        scoreText.text = "Score: " + score;

        PlayerPrefs.SetInt("PotionScore", score);
        PlayerPrefs.SetInt("ActivePotionLevel", activePotionLevel);
        PlayerPrefs.Save();
    }

    private void OnPotionButtonClick(int potionLevel)
    {
        // Add code for handling button clicks here (e.g., switching active potions)
    }

    private void OnLevelButtonClick(int levelToUnlock)
    {
        // Check if the player has enough points to unlock the level
        if (score >= unlockCosts[levelToUnlock - 1])
        {
            // Deduct the cost and unlock the level
            score -= unlockCosts[levelToUnlock - 1];
            activePotionLevel = levelToUnlock;
            UpdateUI();
        }
    }

    private void LevelUp()
    {
        // Player clicks the "Level Up" button
        if (score >= unlockCosts[activePotionLevel])
        {
            // Deduct the cost and unlock the next level
            score -= unlockCosts[activePotionLevel];
            activePotionLevel++;
            UpdateUI();
        }
    }
}
