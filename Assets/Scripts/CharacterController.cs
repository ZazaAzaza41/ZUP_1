using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    //public float groundLevel;  // Уровень земли по Y координате
    [SerializeField] private float jumpForce = 5f; // Сила прыжка
    [SerializeField] private float groundCheckDistance = 0.01f; // Дистанция для проверки земли
    [SerializeField] private float groundCheckTolerance = 0.05f;
    [SerializeField] private LayerMask groundLayer; // Слой, который считается землей

    private Rigidbody rb;

    private bool isGrounded;
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpForce = 25f;
        moveSpeed = 7f;
        jumpHeight = 2.5f;
        //gravity = 1f; // Сила гравитации
        //groundLevel = 0.01f;  // Уровень земли по Y координате
        
    }
    private void Jump()
    {
        // Сбрасываем вертикальную скорость перед прыжком
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // Применяем силу прыжка
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void CheckGrounded()
    {
        // Проверяем Raycast вниз с учетом погрешности
        bool raycastHit = Physics.Raycast(
            transform.position,
            Vector3.down,
            out RaycastHit hit,
            groundCheckDistance,
            groundLayer
        );

        // Если Raycast попал в землю, но расстояние меньше погрешности — считаем, что мы уже "на земле"
        isGrounded = raycastHit && hit.distance <= groundCheckDistance - groundCheckTolerance;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            transform.position + Vector3.down * (groundCheckDistance - groundCheckTolerance),
            transform.position + Vector3.down * groundCheckDistance
        );
    }
    void Update()
    {


        CheckGrounded();


        // Горизонтальное движение
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement_x = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0); // Движение по X
        transform.Translate(movement_x);

        // Вертикальное движение
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement_z = new Vector3(0, 0, verticalInput * moveSpeed * Time.deltaTime); // Движение по Z
        transform.Translate(movement_z);


        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // && transform.position.y <= groundLevel + 0.05 Проверка на уровне земли
        {
            // Начальная вертикальная скорость
            Jump();
        }

        // Гравитация
        //verticalVelocity -= gravity * Time.deltaTime;
        //transform.Translate(new Vector3(0, verticalVelocity * Time.deltaTime, 0)); //Движение по Y

        // Ограничение падения землей
      
    }
}



