using UnityEngine;

public class TriggerLogger : MonoBehaviour
{
    public GameObject obstacle;
    private float pushSpeed = 20f;
    private float deceleration = 2f;
    private Vector3 _currentVelocity;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PushForward();
        }
    }
    public void PushForward()
    {
        _currentVelocity = transform.forward * pushSpeed;
    }
    private void Update()
    {
        if (_currentVelocity.magnitude > 0.01f)
        {
            transform.position += _currentVelocity * Time.deltaTime;
            _currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }
    }

}