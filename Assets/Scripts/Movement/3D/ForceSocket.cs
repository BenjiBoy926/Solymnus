using UnityEngine;

/*
 * CLASS ForceSocket
 * -----------------
 * Simple class allows force to be transferred through it
 * to a force simulated mover
 * -----------------
 */

public class ForceSocket : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Force is applied to this mover when the socket absorbs a force")]
    private ForceSimulatedMover mover;

    public void AbsorbForce(Vector3 direction, float speed, float time)
    {
        mover.ApplyForce(direction, speed, time);
    }
}
