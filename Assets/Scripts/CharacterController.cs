using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    //public float groundLevel;  // ������� ����� �� Y ����������
    [SerializeField] private float jumpForce = 5f; // ���� ������
    [SerializeField] private float groundCheckDistance = 0.01f; // ��������� ��� �������� �����
    [SerializeField] private float groundCheckTolerance = 0.05f;
    [SerializeField] private LayerMask groundLayer; // ����, ������� ��������� ������

    private Rigidbody rb;

    private bool isGrounded;
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpForce = 25f;
        moveSpeed = 7f;
        jumpHeight = 2.5f;
        //gravity = 1f; // ���� ����������
        //groundLevel = 0.01f;  // ������� ����� �� Y ����������
        
    }
    private void Jump()
    {
        // ���������� ������������ �������� ����� �������
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // ��������� ���� ������
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void CheckGrounded()
    {
        // ��������� Raycast ���� � ������ �����������
        bool raycastHit = Physics.Raycast(
            transform.position,
            Vector3.down,
            out RaycastHit hit,
            groundCheckDistance,
            groundLayer
        );

        // ���� Raycast ����� � �����, �� ���������� ������ ����������� � �������, ��� �� ��� "�� �����"
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


        // �������������� ��������
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement_x = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0); // �������� �� X
        transform.Translate(movement_x);

        // ������������ ��������
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement_z = new Vector3(0, 0, verticalInput * moveSpeed * Time.deltaTime); // �������� �� Z
        transform.Translate(movement_z);


        // ������
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // && transform.position.y <= groundLevel + 0.05 �������� �� ������ �����
        {
            // ��������� ������������ ��������
            Jump();
        }

        // ����������
        //verticalVelocity -= gravity * Time.deltaTime;
        //transform.Translate(new Vector3(0, verticalVelocity * Time.deltaTime, 0)); //�������� �� Y

        // ����������� ������� ������
      
    }
}



