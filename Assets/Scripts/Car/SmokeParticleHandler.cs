using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticleHandler : MonoBehaviour
{
    float particleEmissionRate = 0;
    
    CarMovementController carMovementController;
    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    void Awake()
    {
        carMovementController = GetComponentInParent<CarMovementController>();

        particleSystemSmoke = GetComponent<ParticleSystem>();

        particleSystemEmissionModule = particleSystemSmoke.emission;
        particleSystemEmissionModule.rateOverTime = 0;
    }


    void Update()
    {
        //Reduces the particles over time.
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSystemEmissionModule.rateOverTime = particleEmissionRate;

        if (carMovementController.isDrifting)
        {
            particleEmissionRate = 60;   
        }
    }
}


