using UnityEngine;

namespace Assets.Scripts.ZK_Folder
{
    public class AutoSpawnPlatforms : MonoBehaviour
    {
        public GameObject platformPrefab; // Префаб новой платформы 3

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Platform2"))
            {
                // 1. Удаляем стартовую платформу (StartPlatform)
                GameObject startPlatform = GameObject.FindGameObjectWithTag("StartPlatform");
                if (startPlatform != null)
                {
                    Destroy(startPlatform);
                }

                GameObject Platform1 = GameObject.FindGameObjectWithTag("Platform1");
                if (Platform1 != null)
                {
                    Destroy(Platform1);
                    Debug.Log("Удалена стартовая платформа");
                }

                // 2. Platform2 становится Platform1
                other.gameObject.tag = "Platform1";


                // 3. Platform3 становится Platform2
                GameObject platform3 = GameObject.FindGameObjectWithTag("Platform3");
                if (platform3 != null)
                {
                    platform3.tag = "Platform2";

                    // 4. Создаем новую Platform3 перед бывшей Platform3
                    Vector3 newPlatformPosition = platform3.transform.position + new Vector3(0, 0, 40); // Измените смещение по оси Z на нужное
                    GameObject newPlatform = Instantiate(platformPrefab, newPlatformPosition, Quaternion.identity);
                    newPlatform.tag = "Platform3";
                }

            }
        }
    }
}
