using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting; // ВАЖНО: Проверьте, нужен ли этот using (возможно, нет)

namespace Assets.Scripts.ZK_Folder
{
    public class RunnerController : MonoBehaviour
    {
        [Header("Анимация")]
        [Tooltip("Ссылка на компонент Animator")]
        public Animator animator;

        [Header("Движение")]
        [Tooltip("Базовая скорость движения вперед")]
        public float baseSpeed = 0f;
        [Tooltip("Ускорение корабля")]
        public float acceleration = 2f;
        [Tooltip("Максимальная скорость корабля")]
        public float maxSpeed = 20f;
        [Tooltip("Скорость смены полосы движения")]
        public float laneChangeSpeed = 15f;
        [Tooltip("Расстояние между полосами движения")]
        public float laneOffset = 2f;

        [Header("Прыжок/Полет")]
        [Tooltip("Сила подъема при удержании кнопки пробела")]
        public float liftForce = 8f;
        [Tooltip("Множитель гравитации для более быстрого падения")]
        public float gravityMultiplier = 2f;
        [Tooltip("Множитель скорости во время полета")]
        [Range(1f, 3f)] // Ограничиваем значение в пределах 1-3
        public float flySpeedMultiplier = 1.5f;

        [Header("Заземление")]
        [Tooltip("Дистанция проверки наличия земли под кораблем")]
        public float groundCheckDistance = 0.2f;
        [Tooltip("Слой, на котором находится земля")]
        public LayerMask groundLayer;

        [Header("Топливо")]
        [Tooltip("Максимальное количество топлива")]
        public float maxFuel = 100f;
        [Tooltip("Скорость сжигания топлива в секунду во время полета")]
        [Range(0f, 20f)]
        public float fuelBurnRate = 10f;
        [Tooltip("Скорость восстановления топлива в секунду на земле")]
        [Range(0f, 50f)]
        public float fuelRegenRate = 25f;
        [Tooltip("Ссылка на UI Image, отображающий шкалу топлива")]
        public Image fuelBar;

        [Header("Ограничение Полета")]
        [Tooltip("Высота Y, после которой топливо начинает быстро тратиться")]
        public float flightRestrictionY = 10f;
        [Tooltip("Множитель скорости сжигания топлива за пределами зоны")]
        public float outOfBoundsFuelBurnMultiplier = 5f;
        //[Tooltip("Штрафная скорость, когда заканчивается топливо")]
        //public float noFuelSpeedPenalty = 0.5f; // 0.5f - 50% от текущей скорости

        [Header("Кнопки из Меню")]
        public Button startButton;
        public Button exitButton;
        public Button settingsButton;
        public Toggle staticCamera;
        public Button backButton;


        private Rigidbody rb;
        private bool isGrounded;
        private float currentSpeed;
        private int currentLane = 0;
        private float targetPositionX;
        private bool isLifting = false;
        private float currentFuel;
        private bool isFlying = false;
        private bool outOfBounds = false;
        private bool gameStarted = false; // Добавляем флаг для отслеживания состояния игры

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        void Start()
        {
            currentSpeed = baseSpeed;
            targetPositionX = 0f;
            currentFuel = maxFuel;

            if (startButton != null)
            {
                startButton.onClick.AddListener(StartGame);
            }
            currentSpeed = 0f;
            rb.linearVelocity = Vector3.zero;  // Гасим скорость полностью.
            rb.isKinematic = true; //Отключаем физику до старта
        }

        // Метод, который будет вызван при нажатии на кнопку "Старт"
        void StartGame()
        {
            gameStarted = true; // Устанавливаем флаг, что игра началась
            currentSpeed = baseSpeed;  // Возвращаем базовую скорость
            rb.isKinematic = false; //Включаем физику
        }

        private void CheckGrounded()
        {
            Vector3 boxCenter = transform.position;
            Vector3 halfExtents = new Vector3(0.2f, 0.1f, 0.2f);
            isGrounded = Physics.BoxCast(
                boxCenter,
                halfExtents,
                Vector3.down,
                Quaternion.identity,
                groundCheckDistance,
                groundLayer
            );
        }

        void Update()
        {
            // Если игра не началась, выходим из Update()
            if (!gameStarted)
            {
                return;
            }

            CheckGrounded();
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, baseSpeed, maxSpeed);

            // Смена полосы
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("Lturn");
                MoveLane(-1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("Rturn");
                MoveLane(1);
            }

            // Проверка, находится ли игрок за границей (по высоте)
            outOfBounds = transform.position.y > flightRestrictionY;

            // Прыжок/Полет (теперь работает только после нажатия кнопки "Старт")
            isLifting = Input.GetKey(KeyCode.Space);

            // Обновление топлива
            if (isGrounded)
            {
                currentFuel = Mathf.Clamp(currentFuel + fuelRegenRate * Time.deltaTime, 0f, maxFuel);
            }
            else
            {
                float burnRate = fuelBurnRate;
                if (outOfBounds)
                {
                    burnRate *= outOfBoundsFuelBurnMultiplier;
                }
                currentFuel = Mathf.Clamp(currentFuel - burnRate * Time.deltaTime, 0f, maxFuel);
            }

            // Обновление UI шкалы топлива
            if (fuelBar != null)
            {
                fuelBar.fillAmount = currentFuel / maxFuel;
            }

            // Управление скоростью (с учетом топлива)
            //if (currentFuel <= 0f)
            //{
            //    currentSpeed *= noFuelSpeedPenalty;
            //}

            // Анимация
            //animator.SetFloat("Speed", currentSpeed);
        }

        private void FixedUpdate()
        {
            // Движение вперед (теперь работает только после нажатия кнопки "Старт")
            if (gameStarted)
            {
                Vector3 moveDirection = Vector3.forward * currentSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + moveDirection);

                // Боковое движение
                float newX = Mathf.MoveTowards(rb.position.x, targetPositionX, laneChangeSpeed * Time.fixedDeltaTime);
                rb.MovePosition(new Vector3(newX, rb.position.y, rb.position.z));

                // Прыжок/Полет
                if (isLifting && currentFuel > 0f)
                {
                    float currentLiftForce = liftForce;
                    if (!isGrounded)
                    {
                        currentLiftForce *= flySpeedMultiplier;
                    }
                    rb.AddForce(Vector3.up * currentLiftForce);
                }
                // Увеличение гравитации для более быстрого падения
                rb.AddForce(Vector3.down * gravityMultiplier);
            }
        }

        void MoveLane(int direction)
        {
            currentLane += direction;
            currentLane = Mathf.Clamp(currentLane, -1, 1);
            targetPositionX = currentLane * laneOffset;
        }
    }
}
