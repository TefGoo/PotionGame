using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject panel1; // Reference to the first canvas
    public GameObject panel2; // Reference to the second canvas
    public GameObject panel3; // Reference to the third canvas

    public Button button1; // Reference to the first button
    public Button button2; // Reference to the second button
    public Button button3; // Reference to the third button

    private void Start()
    {
        // Initially, only show the first canvas, hide the others
        ShowPanel(panel1);
        HidePanel(panel2);
        HidePanel(panel3);

        // Add listeners to the button click events
        button1.onClick.AddListener(() => ShowPanel(panel1));
        button2.onClick.AddListener(() => ShowPanel(panel2));
        button3.onClick.AddListener(() => ShowPanel(panel3));
    }

    private void ShowPanel(GameObject panelToShow)
    {
        // Activate the specified panel and deactivate the others
        panel1.SetActive(panelToShow == panel1);
        panel2.SetActive(panelToShow == panel2);
        panel3.SetActive(panelToShow == panel3);
    }

    private void HidePanel(GameObject panelToHide)
    {
        // Deactivate the specified panel
        panelToHide.SetActive(false);
    }
}
