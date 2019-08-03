using UnityEngine;
using System.Collections;

/*
 * CLASS AdjustSpeedWhileEmitting
 * ------------------------------
 * Change the speed of a kinematic mover controller
 * between two values - one while the given emitter
 * is emitting and one while it isn't
 * ------------------------------
 */ 

public class AdjustSpeedWhileEmitting : MonoBehaviour
{
    [SerializeField]
    [Tooltip("State activated each time the emitter emits, " +
        "and deactivated once it is ready to emit")]
    private State recentlyEmitted;
    [SerializeField]
    [Tooltip("Script that controls the speed of this object")]
    private KinematicMoverController controller;
    [SerializeField]
    [Tooltip("Speed of the mover controller is scaled by this amount while the emitter is emitting")]
    private float emittingSpeedScalar;
    // True while the scalar is applied
    private bool scalarApplied;
    
    private void Start()
    {
        scalarApplied = false;
        recentlyEmitted.stateActivatedEvent.AddListener(ScaleSpeed);
        recentlyEmitted.stateDeactivatedEvent.AddListener(UnscaleSpeed);
    }
    
    // If a scalar has not been applied yet, apply it
    // Called whenever the emitter emits
    private void ScaleSpeed(float duration)
    {
        if(!scalarApplied)
        {
            controller.AddSpeedScalar(emittingSpeedScalar);
            scalarApplied = true;
        }
    }
    // If a scalar is till applied, remove it
    // Called when the emitted event deactivates
    private void UnscaleSpeed()
    {
        if(scalarApplied)
        {
            controller.RemoveSpeedScalar(emittingSpeedScalar);
            scalarApplied = false;
        }
    }
}
