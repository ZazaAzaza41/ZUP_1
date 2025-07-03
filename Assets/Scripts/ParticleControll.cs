using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleControll : MonoBehaviour
{
    public ParticleSystem ParticleLeft;
    public ParticleSystem ParticleRight;

    [Header("Press Settings")]
    public Gradient pressedGradient;

    [Header("Release Settings")]
    public Gradient releasedGradient;


    private ParticleSystem.ColorOverLifetimeModule colorModuleL;
    private ParticleSystem.ColorOverLifetimeModule colorModuleR;

    void Start()
    {

        if (ParticleLeft != null)
        {
            colorModuleL = ParticleLeft.colorOverLifetime;
            colorModuleL.color = releasedGradient;
        }
        else
        {
            Debug.LogError("ParticleLeft не назначен!");
        }
        
        if (ParticleRight != null)
        {
            colorModuleR = ParticleRight.colorOverLifetime;
            colorModuleR.color = releasedGradient;
        }
        else
        {
            Debug.LogError("ParticleRight не назначен!");
        }

        colorModuleR = ParticleRight.colorOverLifetime;
        colorModuleR.color = releasedGradient;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            colorModuleL.color = pressedGradient;
            colorModuleR.color = pressedGradient;

        }
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            colorModuleL.color = releasedGradient;
            colorModuleR.color = releasedGradient;
            

        }
    }

}
