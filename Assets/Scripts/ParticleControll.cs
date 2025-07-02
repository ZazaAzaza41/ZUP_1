using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleControll : MonoBehaviour
{
    public ParticleSystem ParticleLeft;
    public ParticleSystem ParticleRight;

    [Header("Press Settings")]
    public Gradient pressedGradient;
    public float pressedLifeTime = 2f;

    [Header("Release Settings")]
    public Gradient releasedGradient;
    public float releasedLifeTime = 1f;



    private ParticleSystem.ColorOverLifetimeModule colorModuleL;
    private ParticleSystem.ColorOverLifetimeModule colorModuleR;

    void Start()
    {
        if (ParticleLeft == null)
        {
            ParticleLeft = GetComponent<ParticleSystem>();
        }

        if (ParticleLeft != null)
        {
            colorModuleL = ParticleLeft.colorOverLifetime;
            // Устанавливаем начальный градиент (отпущенное состояние)
            colorModuleL.color = releasedGradient;
        }
        else
        {
            Debug.LogError("Particle System не назначен и не найден на объекте!");
        }
        if (ParticleRight == null)
        {
            ParticleRight = GetComponent<ParticleSystem>();
        }

        if (ParticleRight != null)
        {
            colorModuleR = ParticleRight.colorOverLifetime;
            // Устанавливаем начальный градиент (отпущенное состояние)
            colorModuleR.color = releasedGradient;
        }
        else
        {
            Debug.LogError("Particle System не назначен и не найден на объекте!");
        }
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            colorModuleL.color = pressedGradient;
            //mainModuleL.startLifetime = pressedLifeTime;
            colorModuleR.color = pressedGradient;
            //mainModuleR.startLifetime = pressedLifeTime;

        }
        else if (Input.GetKeyUp(KeyCode.Space)) 
        {
            colorModuleL.color = releasedGradient;
            //mainModuleL.startLifetime = releasedLifeTime;
            colorModuleR.color = releasedGradient;
            //mainModuleR.startLifetime = releasedLifeTime;

        }
    }

}
