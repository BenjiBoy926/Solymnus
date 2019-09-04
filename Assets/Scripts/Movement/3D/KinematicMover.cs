using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS KinematicMover2D
 * ----------------------
 * Enables an object with a 2D rigidbody to move within
 * the x-y plane by setting its velocity
 * ----------------------
 */

public class KinematicMover : MonoBehaviour
{
    /*
     * Public typedefs
     */

    // CLASS Waypoint
    // Pairs a two-point position with the speed at which the object will go to it
    public struct Waypoint
    {
        public Vector3 point { get; private set; }
        public float speed { get; private set; }

        // Basic constructor
        public Waypoint(Vector3 pnt, float spd)
        {
            point = pnt;
            speed = spd;
        }
    }

    /*
     * Editor-set data
     */

    [SerializeField]
    private Rigidbody _rb;
    public Rigidbody rb { get { return _rb; } }

    // Event called when the mover is stopped
    [SerializeField]
    [Tooltip("Event invoked when the mover is stopped")]
    private UnityEvent _stopEvent;
    public UnityEvent stopEvent { get { return _stopEvent; } }

    /*
     * Public interface
     */

    // Set the velocity of the object in the direction specified with the speed specified
    public virtual void MoveTowards(Vector3 direction, float speed)
    {
        MoveTowards(direction.ScaledVector(speed));
    }
    // Set the velocity of the object to the exact vector given
    public virtual void MoveTowards(Vector3 velocity)
    {
        _rb.velocity = velocity;
    }
    // Move in the given direction, but leave the given axes unaffected
    public virtual void MoveTowards(Vector3 direction, float speed, AxisIgnore ignore)
    {
        MoveTowards(direction.ScaledVector(speed), ignore);
    }
    public virtual void MoveTowards(Vector3 velocity, AxisIgnore ignore)
    {
        if (ignore.xIgnore)
        {
            velocity.x = _rb.velocity.x;
        }
        if (ignore.yIgnore)
        {
            velocity.y = _rb.velocity.y;
        }
        if (ignore.zIgnore)
        {
            velocity.z = _rb.velocity.z;
        }
        MoveTowards(velocity);
    }
    // Put the object at a position and set it off in a direction with a particular speed
    public virtual void MoveTowardsFrom(Vector3 origin, Vector3 direction, float speed)
    {
        transform.localPosition = origin;
        MoveTowards(direction, speed);
    }

    // Uses local coroutine to move the object to the point specified,
    // set to arrive at the given time
    public virtual void MoveToPoint(Vector3 point, float time)
    {
        float speed;    // The speed the object needs to go to make it to the point in the time specified
        float dist; // Distance between this object and its destination

        // Calculate the speed the object needs to go
        dist = (point - transform.localPosition).magnitude;
        speed = dist / time;

        // Use calculated speed in the coroutine
        StopCoroutine("SmoothMove");
        StartCoroutine("SmoothMove", new Waypoint(point, speed));
    }

    // Stop coroutines and object motion
    public virtual void Stop()
    {
        StopAllCoroutines();
        _rb.velocity = Vector3.zero;
        _stopEvent.Invoke();
    }

    /*
     * Private helpers
     */

    // Smoothly moves the object to the point, then stops
    // WARNING: the algorithm breaks if something gets in the way of the object trying to move
    private IEnumerator SmoothMove(Waypoint point)
    {
        Func<bool> IsSufficientlyClose; // Delegate returns true when the object is sufficiently close to the target

        // Set velocity towards the direction
        MoveTowards(point.point - transform.localPosition, point.speed);

        // Set delegate to return true when the difference between
        // destination and current position is negligibly small
        IsSufficientlyClose = delegate {
            return (point.point - transform.localPosition).sqrMagnitude < 0.001f;
        };

        // Wait till object is close to the point, then stop moving
        yield return new WaitUntil(IsSufficientlyClose);
        Stop();
    }
}

