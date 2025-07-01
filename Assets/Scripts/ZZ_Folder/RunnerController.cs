using UnityEngine;
using UnityEngine.UI;
using System;



namespace Assets.Scripts.ZZ_Folder
{
    public class RunnerController : MonoBehaviour
    {
        [Header("Анимация")]
        [Tooltip("Ссылка на компонент Animator")]
        public Animator animator;

        [Header("Движение")]
        [Tooltip("Базовая скорость движения вперед")]
        public float baseSpeed = 10f;
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

        [Header("HP")]
        [Tooltip("Максимальное количество HP")]
        public float maxHP = 3f;
        [Tooltip("Ссылка на UI Image, отображающий шкалу HP")]
        public Image HPBar;

        [Header("Ограничение Полета")]
        [Tooltip("Высота Y, после которой топливо начинает быстро тратиться")]
        public float flightRestrictionY = 10f;
        [Tooltip("Множитель скорости сжигания топлива за пределами зоны")]
        public float outOfBoundsFuelBurnMultiplier = 5f;
        //[Tooltip("Штрафная скорость, когда заканчивается топливо")]
        //public float noFuelSpeedPenalty = 0.5f; // 0.5f - 50% от текущей скорости

        private Rigidbody rb;
        private bool isGrounded;
        private float currentSpeed;
        private int currentLane = 0;
        private float targetPositionX;
        private bool isLifting = false;
        private float currentFuel;
        private float currentHP;
        private bool isFlying = false;
        private bool outOfBounds = false;

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
            currentHP = maxHP;
            UpdateFuelUI();
            UpdateHPUI();
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
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                ChangeHP();
            }
        }
        void Update()
        {
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

            // Прыжок / Полет
            if (Input.GetKey(KeyCode.Space) && currentFuel > 0)
            {
                isLifting = true;
                isFlying = true;
            }
            else
            {
                isLifting = false;
                isFlying = false;
            }

            // Управление топливом
            HandleFuel();

            UpdateFuelUI();
            
            // Обновляем UI в каждом кадре
            UpdateHPUI();
            // Анимация полета---------------------------------------------------------------------------
            //animator.SetBool("isFlying", isFlying);
        }

        void FixedUpdate()
        {
            // Движение по полосам

            // Движение вперед
            Vector3 forwardMovement = transform.forward * currentSpeed * Time.fixedDeltaTime;

            // Применение подъема (полета)
            if (isLifting)
            {
                rb.AddForce(Vector3.up * liftForce);
                forwardMovement *= flySpeedMultiplier; // Увеличиваем скорость вперед во время полета
            }

            // Применение гравитации
            if (!isGrounded)
            {
                rb.AddForce(Vector3.down * Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime);
            }

            rb.MovePosition(rb.position + forwardMovement);

            Vector3 newPosition = rb.position;
            newPosition.x = Mathf.MoveTowards(rb.position.x, targetPositionX, laneChangeSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);

        }

        void MoveLane(int direction)
        {
            currentLane += direction;
            currentLane = Mathf.Clamp(currentLane, -1, 1); // Ограничиваем полосы (-1, 0, 1)
            targetPositionX = currentLane * laneOffset;
        }

        void HandleFuel()
        {
            if (isFlying)
            {
                float burnRate = fuelBurnRate;

                // Увеличиваем расход топлива за границей
                if (outOfBounds)
                {
                    burnRate *= outOfBoundsFuelBurnMultiplier;
                }

                currentFuel -= burnRate * Time.deltaTime;
                currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
            }
            else if (isGrounded)
            {
                currentFuel += fuelRegenRate * Time.deltaTime;
                currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
            }
        }

        public void ChangeHP()
        {
            currentHP -= 1f;
        }
        void UpdateFuelUI()
        {
            if (fuelBar != null)
            {
                fuelBar.fillAmount = currentFuel / maxFuel;
            }
        }
        void UpdateHPUI()
        {
            if (HPBar != null)
            {
                HPBar.fillAmount = currentHP / maxHP;
            }
        }
    }
}
