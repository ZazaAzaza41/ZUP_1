//using UnityEngine;

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
//        public float jumpHeight;

//        //public float groundLevel;  // Уровень земли по Y координате
//        [SerializeField] private float jumpForce = 5f; // Сила прыжка
//        [SerializeField] private float groundCheckDistance = 0.01f; // Дистанция для проверки земли
//        [SerializeField] private float groundCheckTolerance = 0.05f;
//        [SerializeField] private LayerMask groundLayer; // Слой, который считается землей       

//        private Rigidbody rb;

//        private bool isGrounded;

//        private float currentSpeed;
//        private int currentLane = 0;
//        private float targetPositionX;

//        public void Awake()
//        {
//            rb = GetComponent<Rigidbody>();
//            jumpForce = 25f;
//            jumpHeight = 2.5f;
//            //gravity = 1f; // Сила гравитации
//            //groundLevel = 0.01f;  // Уровень земли по Y координате

//        }
//        void Start()
//        {
//            currentSpeed = baseSpeed;
//            targetPositionX = 0f;
//        }
//        private void Jump()
//        {
//            // Сбрасываем вертикальную скорость перед прыжком
//            //rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

//            // Применяем силу прыжка
//            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//        }

//        private void CheckGrounded()
//        {
//            // Проверяем Raycast вниз с учетом погрешности
//            bool raycastHit = Physics.Raycast(
//                transform.position,
//                Vector3.down,
//                out RaycastHit hit,
//                groundCheckDistance,
//                groundLayer
//            );

//            // Если Raycast попал в землю, но расстояние меньше погрешности — считаем, что мы уже "на земле"
//            isGrounded = raycastHit && hit.distance <= groundCheckDistance - groundCheckTolerance;
//        }
//        private void OnDrawGizmosSelected()
//        {
//            Gizmos.color = Color.red;
//            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

//            Gizmos.color = Color.yellow;
//            Gizmos.DrawLine(
//                transform.position + Vector3.down * (groundCheckDistance - groundCheckTolerance),
//                transform.position + Vector3.down * groundCheckDistance
//            );
//        }

//        void Update()
//        {
//            CheckGrounded();
//            // Ускорение
//            currentSpeed += acceleration * Time.deltaTime;
//            currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed);

//            // Движение вперед
//            transform.position += transform.forward * currentSpeed * Time.deltaTime;

//            // Плавное перестроение по X
//            float newX = Mathf.MoveTowards(transform.position.x, targetPositionX, laneChangeSpeed * Time.deltaTime);
//            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

//            // Обработка смены полосы
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
//            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // && transform.position.y <= groundLevel + 0.05 Проверка на уровне земли
//            {
//                // Начальная вертикальная скорость
//                Jump();
//            }
//        }

//        void MoveLane(int direction)
//        {
//            currentLane += direction;
//            currentLane = Mathf.Clamp(currentLane, -1, 1);
//            targetPositionX = currentLane * laneOffset;
//        }
//    }
//}

using UnityEngine;

namespace Assets.Scripts.ZK_Folder
{
    public class RunnerController : MonoBehaviour
    {
        public Animator animator;
        public float baseSpeed = 10f;
        public float acceleration = 2f;
        public float maxSpeed = 20f;
        public float laneChangeSpeed = 15f;
        public float laneOffset = 2f;
        public float jumpForce = 8f; // Оптимальное значение силы прыжка
        public float groundCheckDistance = 0.2f; // Увеличена дистанция проверки
        public LayerMask groundLayer;

        private Rigidbody rb;
        private bool isGrounded;
        private float currentSpeed;
        private int currentLane = 0;
        private float targetPositionX;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            // Отключаем вращение по осям для стабильности
            rb.freezeRotation = true;
        }

        void Start()
        {
            currentSpeed = baseSpeed;
            targetPositionX = 0f;
        }

        private void Jump()
        {
            // Сбрасываем вертикальную скорость перед прыжком
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private void CheckGrounded()
        {
            // Используем BoxCast для более точного определения земли
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

            // Для отладки
            //Debug.DrawRay(transform.position, Vector3.down * (groundCheckDistance + halfExtents.y),
            //    isGrounded ? Color.green : Color.red);
        }

        void Update()
        {
            CheckGrounded();
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, baseSpeed, maxSpeed);

            // Обработка смены полосы
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
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }

        void FixedUpdate()
        {
            // Движение вперед через velocity (синхронизировано с физикой)
            Vector3 newVelocity = new Vector3(
                (targetPositionX - transform.position.x) * laneChangeSpeed,
                rb.linearVelocity.y,
                currentSpeed
            );
            rb.linearVelocity = newVelocity;
        }

        void MoveLane(int direction)
        {
            currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
            targetPositionX = currentLane * laneOffset;
        }

        // Визуализация зоны проверки земли
        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.blue;
        //    Vector3 boxCenter = transform.position + Vector3.down * groundCheckDistance / 2;
        //    Vector3 size = new Vector3(0.4f, groundCheckDistance, 0.4f);
        //    Gizmos.DrawWireCube(boxCenter, size);
        //}
    }
}