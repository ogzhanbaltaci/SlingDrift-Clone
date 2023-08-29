using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTireTrail : MonoBehaviour
{
    CarMovementController carMovementController;
    TrailRenderer trailRenderer;
    void Awake()
    {
        carMovementController = GetComponentInParent<CarMovementController>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    void Update()
    {
        if(carMovementController.isDrifting)
            trailRenderer.emitting = true;
        else trailRenderer.emitting = false;

    }
}
