﻿using UnityEngine;

/*
 * CLASS ForceSimulatedMover2D
 * ---------------------------
 * A type of mover that can simulate forces by locking movement
 * calls while being forced to move in a direction. Great for
 * causing a kinematic mover to "jolt" in a particular direction
 * and prevent it from immediately re-directing its velocity
 * --------------------------
 */

public class ForceSimulatedMover2D : KinematicMover2D
{
    public const float DEFAULT_BLOWBACK_TIME = 0.2f;    // Time for which all blowback movers are blown back

    [SerializeField]
    [Tooltip("State script helps manage when the mover is being forced to move in a direction")]
    private State _isForced;
    public State isForced { get { return _isForced; } }
    private Vector2 fromPointToHere = new Vector2();    // Points from the point of blowback to the position of this mover

    // Move in the direction, speed, and for the time specifed
    // Additional functionality locks movement of the mover until blowback is finished
    public void ApplyForce(Vector2 direction, float speed, float time = DEFAULT_BLOWBACK_TIME)
    {
        // Disable the blownback state and move according to the move towards method
        if(_isForced)
        {
            Stop();
        }
        MoveTowards(direction, speed);

        // Activate the blowback state and schedule the object to stop moving when finished
        _isForced.Activate(time);
        CancelInvoke();
        Invoke("Stop", time);
    }

    // Blowback methods blowback the mover from the specified point in world space, rather than with the specific direction
    public void ApplyForceFromPoint(Vector2 point, float speed, float time = DEFAULT_BLOWBACK_TIME)
    {
        fromPointToHere = (Vector2)transform.position - point;
        ApplyForce(fromPointToHere, speed, time);
    }

    // Stop being blown back, and stop movement
    public override void Stop()
    {
        CancelInvoke();
        _isForced.Deactivate();
        base.Stop();
    }

    // Overrides of the base class methods prevent client code from moving this object 
    // if it is currently being blown back
    public override void MoveTowards(Vector2 direction, float speed)
    {
        if(!_isForced)
        {
            base.MoveTowards(direction, speed);
        }
    }
    public override void MoveTowards(Vector2 velocity)
    {
        if(!_isForced)
        {
            base.MoveTowards(velocity);
        }
    }
    public override void MoveToPoint(Vector2 point, float time)
    {
        if(!_isForced)
        {
            base.MoveToPoint(point, time);
        }
    }
    public override void MoveTowardsFrom(Vector2 origin, Vector2 direction, float speed)
    {
        if(!_isForced)
        {
            base.MoveTowardsFrom(origin, direction, speed);
        }
    }

    // "Override" methods allow client code the option to force the object to move regardless of whether or not it is being blown back right now
    public void OverrideMoveTowards(Vector2 direction, float speed)
    {
        _isForced.Deactivate();
        base.MoveTowards(direction, speed);
    }
    public void OverrideMoveTowards(Vector2 velocity)
    {
        _isForced.Deactivate();
        base.MoveTowards(velocity);
    }
    public void OverrideMoveToPoint(Vector2 point, float time)
    {
        _isForced.Deactivate();
        base.MoveToPoint(point, time);
    }
    public void OverrideMoveTowardsFrom(Vector2 origin, Vector2 direction, float speed)
    {
        _isForced.Deactivate();
        base.MoveTowardsFrom(origin, direction, speed);
    }
}
