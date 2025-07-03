using UnityEngine;
using UnityEngine.UI; // Необходим для работы с UI-элементами

public class CameraRotationButton : MonoBehaviour
{
    public float rotationSpeed = 45f;// Скорость поворота
    public Button startButton;
    public GameObject gameUI;
    public GameObject gamePause;


    private bool isRotating = false;// Флаг, указывающий, нужно ли вращать
    private Quaternion targetRotation;// Целевая ротация

    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartRotation);
        }

        //targetRotation = Quaternion.Euler(0, 0, 0); //поворот в точные координаты
        targetRotation = transform.rotation * Quaternion.Euler(22, -180, 0); //поворот относительно текущей ротации

        {
            gameUI.SetActive(false);
            gamePause.SetActive(false);
        }
    }

    void Update()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (gameUI != null)
            {
                gameUI.SetActive(true);
                gamePause.SetActive(true);
            }
        }
    }
    public void StartRotation()
    {
        isRotating = true;
    }
}
