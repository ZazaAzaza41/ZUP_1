using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraRotationButton : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public Button startButton;
    public Button pauseButton;
    public Button resumeButton;
    public Button mainmenuButton;
    public Button restartButton;
    public GameObject gameUI;
    public GameObject gamePauseIcon;
    public GameObject gamePauseMenu;

    private bool isRotating = false;
    private Quaternion targetRotation;
    private bool isPaused = false;
    private Quaternion originalRotation;

    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartRotation);
        }
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }
        if (mainmenuButton != null)
        {
            mainmenuButton.onClick.AddListener(GoToMainMenu);
        }
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(GoToMainMenu);
        }

        gameUI.SetActive(false);
        gamePauseIcon.SetActive(false);
        gamePauseMenu.SetActive(false);

        originalRotation = transform.rotation; // Сохраняем начальную ротацию
    }

    void Update()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //закончено ли вращение (важно для плавной работы)!!!!!
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isRotating = false;
            }
        }
    }

    public void StartRotation()
    {
        isRotating = true;
        targetRotation = transform.rotation * Quaternion.Euler(22, -180, 0);
        gameUI.SetActive(true);
        gamePauseIcon.SetActive(true);
    }

    // Функция для переключения состояния паузы (вкл/выкл)
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        isRotating = false;
        gamePauseMenu.SetActive(true);
        gameUI.SetActive(false);
        gamePauseIcon.SetActive(false);
        Time.timeScale = 0f;    
    }

    void ResumeGame()
    {
        gamePauseMenu.SetActive(false);
        gameUI.SetActive(true);
        gamePauseIcon.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ZK2_0");
    }
}
