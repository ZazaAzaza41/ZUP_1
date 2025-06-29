using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpHeight = 2f;
        public float gravity = 9.81f; // ���� ����������
        public float groundLevel = 0f;  // ������� ����� �� Y ����������
        private float verticalVelocity = 1f; // ������������ ��������

        void Update()
        {
            // �������������� ��������
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 movement_x = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0); // �������� �� X
            transform.Translate(movement_x);

            // ������������ ��������
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement_z = new Vector3(0, 0, verticalInput * moveSpeed * Time.deltaTime); // �������� �� Z
            transform.Translate(movement_z);



            // ������
            if (Input.GetKeyDown(KeyCode.Space) && transform.position.y <= groundLevel) // �������� �� ������ �����
            {
                verticalVelocity = Mathf.Sqrt(2 * jumpHeight * gravity); // ��������� ������������ ��������
            }

            // ����������
            verticalVelocity -= gravity * Time.deltaTime;
            transform.Translate(new Vector3(0, verticalVelocity * Time.deltaTime, 0)); //�������� �� Y

            // ����������� ������� ������
            if (transform.position.y < groundLevel)
            {
                transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);
                verticalVelocity = 0;
            }
        }
    }
}




