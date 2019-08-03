using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS Emitter2p5D
 * -----------------
 * Emitter in 2.5d space. Receives 2D emission vectors and anchors,
 * but the motion of the emitted objects is in a plane in 3-D space
 * -----------------
 */ 

public class Emitter2p5D : MonoBehaviour, IEmitter2D
{
    /*
     * Editor data
     */

    [SerializeField]
    [Tooltip("Prefab to instantiate and emit")]
    private GameObject emittedObject;
    [SerializeField]
    [Tooltip("Normal to the 3D plane of emission")]
    private Vector3 planeNormal;
    [SerializeField]
    [Tooltip("Speed at which the objects are emitted")]
    private float _objectVelocity;
    [SerializeField]
    [Tooltip("Determine origin and direction of objects emitted relative to the given aim vector. " +
        "Objects anchored to the right go along the aim vector")]
    private List<Anchor2D> objectAnchors;
    [SerializeField]
    [Tooltip("Set of events invoked when the emitter emits")]
    private EmissionEvent2D _emissionEvent;
    public EmissionEvent2D emissionEvent { get { return _emissionEvent; } }

    /*
     * Private data
     */
    
    private ObjectPool<Rigidbody> pool;   // Emitter2D's object pool

    /*
     * Public interface
     */

    // Emit the objects using the local information
    // Aim vector is used such that objects going straight to the right go along the aim vector
    public virtual void Emit(Vector2 aimVector)
    {
        Vector2 localOrigin;  // Origin of the current bullet, rotated by the aim vector
        Vector2 force;   // Direction of the current bullet, rotated by the aim vector
        float tiltAngle;    // Angle of the aim vector from the right

        tiltAngle = Vector2.SignedAngle(Vector2.right, aimVector);

        // Rotate all origins and directions in the anchors by the tilt angle,
        // and add an impulse force to an object in the pool using rotated vectors
        foreach (Anchor2D anchor in objectAnchors)
        {
            localOrigin = anchor.origin.RotatedVector(tiltAngle);
            force = anchor.direction.RotatedVector(tiltAngle).ScaledVector(_objectVelocity);
            LaunchBody(pool.getOneQuick, localOrigin, force);
        }

        _emissionEvent.Invoke(aimVector);
    }

    public void Consume(Vector2 aimVector)
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
    private void LaunchBody(Rigidbody body, Vector2 localOrigin, Vector2 force)
    {
        body.gameObject.SetActive(true);
        body.transform.position = transform.position + localOrigin.MapToPlane(planeNormal);
        body.velocity = force.MapToPlane(planeNormal);
    }
}
