using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpHeight = 2f;
        public float gravity = 9.81f; // Сила гравитации
        public float groundLevel = 0f;  // Уровень земли по Y координате
        private float verticalVelocity = 1f; // Вертикальная скорость

        void Update()
        {
            // Горизонтальное движение
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 movement_x = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0); // Движение по X
            transform.Translate(movement_x);

            // Вертикальное движение
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 movement_z = new Vector3(0, 0, verticalInput * moveSpeed * Time.deltaTime); // Движение по Z
            transform.Translate(movement_z);



            // Прыжок
            if (Input.GetKeyDown(KeyCode.Space) && transform.position.y <= groundLevel) // Проверка на уровне земли
            {
                verticalVelocity = Mathf.Sqrt(2 * jumpHeight * gravity); // Начальная вертикальная скорость
            }

            // Гравитация
            verticalVelocity -= gravity * Time.deltaTime;
            transform.Translate(new Vector3(0, verticalVelocity * Time.deltaTime, 0)); //Движение по Y

            // Ограничение падения землей
            if (transform.position.y < groundLevel)
            {
                transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);
                verticalVelocity = 0;
            }
        }
    }
}




