using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public Button startButton;

    void Start()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
    }

    void StartGame()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }
    }
}
