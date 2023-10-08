using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionStore : MonoBehaviour
{
    public TMP_Text costText;
    public TMP_Text levelText;

    public int[] potionUnlockCosts = { 0, 20, 130, 400, 1000, 2500, 6000, 15000, 40000 };
    public Button[] potionButtons; // Array to store the potion buttons

    private int score;
    private int activePotionLevel = 1;

    private void Start()
    {
        // Load score and activePotionLevel from PlayerPrefs
        score = PlayerPrefs.GetInt("PotionScore", 0);
        activePotionLevel = PlayerPrefs.GetInt("ActivePotionLevel", 1);

        // Attach button click listeners for all potions using the array
        for (int i = 0; i < potionButtons.Length; i++)
        {
            int potionLevel = i + 1; // Potion levels start from 1
            Button button = potionButtons[i];

            button.onClick.AddListener(() =>
            {
                OnPotionButtonClick(potionLevel);
            });
        }

        // Initial UI update
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update cost and level TextMeshPro Text components
        costText.text = "Cost: " + potionUnlockCosts[activePotionLevel];
        levelText.text = "Level: " + activePotionLevel;
    }

    private void OnPotionButtonClick(int potionLevel)
    {
        int unlockCost = potionUnlockCosts[potionLevel];

        if (score >= unlockCost && potionLevel > activePotionLevel)
        {
            // Buy the potion
            activePotionLevel = potionLevel;
            score -= unlockCost;

            // Save score and activePotionLevel to PlayerPrefs
            PlayerPrefs.SetInt("PotionScore", score);
            PlayerPrefs.SetInt("ActivePotionLevel", activePotionLevel);
            PlayerPrefs.Save();

            // Update UI
            UpdateUI();
        }
    }
}
