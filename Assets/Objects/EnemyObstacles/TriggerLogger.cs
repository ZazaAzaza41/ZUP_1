using UnityEngine;

public class TriggerLogger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Игрок вошёл в триггер '{gameObject.name}'");
        }
    }

}