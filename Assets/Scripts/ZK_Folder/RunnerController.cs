//using UnityEngine;
//using UnityEngine.UI; // Для работы с UI элементами

//namespace Assets.Scripts.ZK_Folder
//{
//    public class RunnerController : MonoBehaviour
//    {
//        public Animator animator;
//        public float baseSpeed = 10f;
//        public float acceleration = 2f;
//        public float maxSpeed = 20f;
//        public float laneChangeSpeed = 15f;
//        public float laneOffset = 2f;
//        public float liftForce = 8f;
//        public float gravityMultiplier = 2f;
//        public float groundCheckDistance = 0.2f;
//        public LayerMask groundLayer;

//        public float maxFuel = 100f; // Максимальное количество топлива
//        public float fuelBurnRate = 10f; // Скорость сжигания топлива в секунду
//        public float fuelRegenRate = 5f; // Скорость восстановления топлива в секунду
//        public float flySpeedMultiplier = 1.5f; // Множитель скорости полета

//        public Image fuelBar; // Ссылка на UI Image (Fuel Bar)

//        private Rigidbody rb;
//        private bool isGrounded;
//        private float currentSpeed;
//        private int currentLane = 0;
//        private float targetPositionX;
//        private bool isLifting = false;
//        private float currentFuel;
//        private bool isFlying = false;

//        private void Awake()
//        {
//            rb = GetComponent<Rigidbody>();
//            rb.freezeRotation = true;
//        }

//        void Start()
//        {
//            currentSpeed = baseSpeed;
//            targetPositionX = 0f;
//            currentFuel = maxFuel;
//            //UpdateFuelUI();
//        }

//        private void CheckGrounded()
//        {
//            Vector3 boxCenter = transform.position;
//            Vector3 halfExtents = new Vector3(0.2f, 0.1f, 0.2f);
//            isGrounded = Physics.BoxCast(
//                boxCenter,
//                halfExtents,
//                Vector3.down,
//                Quaternion.identity,
//                groundCheckDistance,
//                groundLayer
//            );
//        }

//        void Update()
//        {
//            CheckGrounded();
//            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, baseSpeed, maxSpeed);

//            // Смена полосы
//            if (Input.GetKeyDown(KeyCode.A))
//            {
//                animator.SetTrigger("Lturn");
//                MoveLane(-1);
//            }
//            if (Input.GetKeyDown(KeyCode.D))
//            {
//                animator.SetTrigger("Rturn");
//                MoveLane(1);
//            }

//            // Подъем/полет
//            if (Input.GetKey(KeyCode.Space))
//            {
//                if (isGrounded)
//                {
//                    isLifting = true;
//                    isFlying = false;
//                }
//                else if (currentFuel > 0)
//                {
//                    isLifting = false;
//                    isFlying = true;
//                }
//                else
//                {
//                    isLifting = false;
//                    isFlying = false;
//                }
//            }
//            else
//            {
//                isLifting = false;
//                isFlying = false;
//            }

//            // Управление топливом
//            if (isFlying)
//            {
//                currentFuel -= fuelBurnRate * Time.deltaTime;
//                currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
//            }
//            else if (isGrounded && currentFuel < maxFuel)
//            {
//                currentFuel += fuelRegenRate * Time.deltaTime;
//                currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
//            }

//            //UpdateFuelUI();
//        }

//        void FixedUpdate()
//        {
//            // Движение вперед
//            float currentForwardSpeed = currentSpeed;
//            if (isFlying)
//            {
//                currentForwardSpeed *= flySpeedMultiplier;
//            }

//            Vector3 newVelocity = new Vector3(
//                (targetPositionX - transform.position.x) * laneChangeSpeed,
//                rb.linearVelocity.y,
//                currentForwardSpeed
//            );
//            rb.linearVelocity = newVelocity;

//            // Подъем
//            if (isLifting)
//            {
//                rb.AddForce(Vector3.up * liftForce, ForceMode.Acceleration);
//            }

//            // Полет
//            if (isFlying)
//            {
//                // Поддерживаем высоту во время полета.  Вы можете это изменить, чтобы позволить игроку падать.
//                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Устанавливаем вертикальную скорость в 0
//                rb.AddForce(Vector3.up * liftForce, ForceMode.Acceleration); // Поддерживаем высоту

//            }

//            // Дополнительная гравитация
//            if (!isGrounded && !isFlying) // Не применяем, когда летим.
//            {
//                rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
//            }
//        }

//        void MoveLane(int direction)
//        {
//            currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
//            targetPositionX = currentLane * laneOffset;
//        }

//        //void UpdateFuelUI()
//        //{
//        //    if (fuelBar != null)
//        //    {
//        //        fuelBar.fillAmount = currentFuel / maxFuel;
//        //    }
//        //}
//    }
//}



using UnityEngine;
using UnityEngine.UI;
using UnityEditor; // Только для Editor скриптов
using System;

namespace Assets.Scripts.ZK_Folder
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
        [Range(0f, 20f)] // Ограничиваем значение в пределах 0-20
        public float fuelBurnRate = 10f;
        [Tooltip("Скорость восстановления топлива в секунду на земле")]
        [Range(0f, 10f)] // Ограничиваем значение в пределах 0-10
        public float fuelRegenRate = 5f;
        [Tooltip("Ссылка на UI Image, отображающий шкалу топлива")]
        public Image fuelBar;

        private Rigidbody rb;
        private bool isGrounded;
        private float currentSpeed;
        private int currentLane = 0;
        private float targetPositionX;
        private bool isLifting = false;
        private float currentFuel;
        private bool isFlying = false;

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
            UpdateFuelUI();
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

            // Подъем/Полет
            if (Input.GetKey(KeyCode.Space))
            {
                if (isGrounded)
                {
                    isLifting = true;
                    isFlying = false;
                }
                else if (currentFuel > 0)
                {
                    isLifting = false;
                    isFlying = true;
                }
                else
                {
                    isLifting = false;
                    isFlying = false;
                }
            }
            else
            {
                isLifting = false;
                isFlying = false;
            }

            // Управление топливом
            if (isFlying)
            {
                currentFuel -= fuelBurnRate * Time.deltaTime;
                currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
            }
            else if (isGrounded && currentFuel < maxFuel)
            {
                currentFuel += fuelRegenRate * Time.deltaTime;
                currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
            }

            UpdateFuelUI();
        }

        void FixedUpdate()
        {
            // Движение вперед
            float currentForwardSpeed = currentSpeed;
            if (isFlying)
            {
                currentForwardSpeed *= flySpeedMultiplier;
            }

            Vector3 newVelocity = new Vector3(
                (targetPositionX - transform.position.x) * laneChangeSpeed,
                rb.linearVelocity.y,
                currentForwardSpeed
            );
            rb.linearVelocity = newVelocity;

            // Подъем
            if (isLifting)
            {
                rb.AddForce(Vector3.up * liftForce, ForceMode.Acceleration);
            }

            // Полет
            if (isFlying)
            {
                // Поддерживаем высоту во время полета.  Вы можете это изменить, чтобы позволить игроку падать.
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Устанавливаем вертикальную скорость в 0
                rb.AddForce(Vector3.up * liftForce, ForceMode.Acceleration); // Поддерживаем высоту
            }

            // Дополнительная гравитация
            if (!isGrounded && !isFlying) // Не применяем, когда летим.
            {
                rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
            }
        }

        void MoveLane(int direction)
        {
            currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
            targetPositionX = currentLane * laneOffset;
        }

        void UpdateFuelUI()
        {
            if (fuelBar != null)
            {
                fuelBar.fillAmount = currentFuel / maxFuel;
            }
        }
    }
}
