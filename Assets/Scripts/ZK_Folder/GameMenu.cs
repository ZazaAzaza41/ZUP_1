using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject shopPanel;
    public Button startButton;
    public Button shopButton;
    public Button backButton;

    // ������ ��� ��������� ��������� �������
    public Button purpleShipButton;
    public Button goldShipButton;
    public Button blackShipButton;

    // ������ �� Renderer �������.  ��������� ��� � ����������!
    public Renderer shipRenderer;

    // ��������� ��� �������
    public Material purpleShipMaterial;
    public Material goldShipMaterial;
    public Material blackShipMaterial;


    void Start()
    {
        // �������� �� null � ��������� �������
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
        // �������� �� ������� ������
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
        if (shopButton != null)
        {
            shopButton.onClick.AddListener(OpenShop);
        }
        if (backButton != null)
        {
            backButton.onClick.AddListener(BackToMainMenu);
        }
        // �������� �� ������� ������ ��������� ���������
        if (purpleShipButton != null)
        {
            purpleShipButton.onClick.AddListener(SetShipMaterialPurple);
        }
        if (goldShipButton != null)
        {
            goldShipButton.onClick.AddListener(SetShipMaterialGold);
        }
        if (blackShipButton != null)
        {
            blackShipButton.onClick.AddListener(SetShipMaterialBlack);
        }
    }
    void OpenShop()
    {
        mainMenuPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        shopPanel.SetActive(false);
    }

    void StartGame()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }
    }
    void SetShipMaterialPurple()
    {
        if (shipRenderer != null && purpleShipMaterial != null)
        {
            shipRenderer.material = purpleShipMaterial;
        }
    }

    void SetShipMaterialGold()
    {
        if (shipRenderer != null && goldShipMaterial != null)
        {
            shipRenderer.material = goldShipMaterial;
        }
    }

    void SetShipMaterialBlack()
    {
        if (shipRenderer != null && blackShipMaterial != null)
        {
            shipRenderer.material = blackShipMaterial;
        }
    }
}
