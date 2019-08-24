using UnityEngine;
using System.Collections.Generic;

public class Emitter : MonoBehaviour, IEmitter
{
    /*
 * Editor data
 */

    [SerializeField]
    private GameObject emittedObject;
    [SerializeField]
    private float _objectVelocity;   // Speed at which objects travel
    [SerializeField]
    private List<Anchor> objectAnchors; // Used to determine the local origin the objects start at and the direction they are fired off in relative to the emitter's aim 
    [SerializeField]
    [Tooltip("Set of events invoked when the emitter emits")]
    private EmissionEvent _emissionEvent;    // Event called whenever the the emitter emits
    public EmissionEvent emissionEvent { get { return _emissionEvent; } }

    /*
     * Private data
     */
    private ObjectPool<Rigidbody> pool;   // Emitter2D's object pool

    /*
     * Public interface
     */

    // Emit the objects using the local information
    // Aim vector is used such that objects going straight to the right go along the aim vector
    public virtual void Emit(Vector3 aimVector)
    {
        Vector3 localOrigin;  // Origin of the current bullet, rotated by the aim vector
        Vector3 force;   // Direction of the current bullet, rotated by the aim vector

        // Rotate all origins and directions in the anchors by the tilt angle,
        // and add an impulse force to an object in the pool using rotated vectors
        foreach (Anchor anchor in objectAnchors)
        {
            localOrigin = anchor.origin.Transform(Vector3.forward, aimVector);
            force = anchor.direction.Transform(Vector3.forward, aimVector).ScaledVector(_objectVelocity);
            LaunchBody(pool.getOneQuick, localOrigin, force);
        }

        _emissionEvent.Invoke(aimVector);
    }

    public void Consume(Vector3 aimVector)
    {
        Emit(aimVector);
    }

    /*
     * Private helpers
     */

    protected virtual void Start()
    {
        pool = new ObjectPool<Rigidbody>(emittedObject, gameObject.name + "'s Pool");
    }

    // Simple helper moves the body to the position relative to this object 
    // and sets it off with an initial velocity
    private void LaunchBody(Rigidbody body, Vector3 localOrigin, Vector3 force)
    {
        body.gameObject.SetActive(true);
        body.transform.position = transform.position + localOrigin;
        body.velocity = force;
    }
}
