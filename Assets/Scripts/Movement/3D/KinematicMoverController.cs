﻿using UnityEngine;

/*
 * CLASS KinematicMoverController
 * ------------------------------
 * Base class for any script that intends to take control
 * of a kinematic mover
 * ------------------------------
 */

public class KinematicMoverController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Script the controller uses to move the object around")]
    private KinematicMover mover;
    [SerializeField]
    [Tooltip("Base speed at which the controller moves the object around")]
    private float baseSpeed;
    private float speedScalar = 1f;  // Current scalar applied to the speed

    // Get the base speed times the current speed scalar
    public float speed
    {
        get
        {
            return baseSpeed * speedScalar;
        }
    }

    public void Move(Vector3 dir)
    {
        mover.MoveTowards(dir, speed);
    }
    public void Move(Vector3 dir, AxisIgnore ignore)
    {
        mover.MoveTowards(dir, speed, ignore);
    }

    // Multiply the given scalar by the speed scalar
    public void AddSpeedScalar(float scalar)
    {
        if (scalar >= 0f)
        {
            speedScalar *= scalar;
        }
    }
    // Remove a scalar constant by dividing it out of the current scalar
    public void RemoveSpeedScalar(float scalar)
    {
        if (scalar > 0f)
        {
            speedScalar /= scalar;
        }
    }
}
