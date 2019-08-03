using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS ForceSource
 * -----------------
 * Transfers force to any force socket it comes into contact with
 * The direction of the force points away from the source towards the socket
 * -----------------
 */ 

public class ForceSource2D : CollisionEnterComponentProcessor<ForceSocket2D>
{
    // Allows event to be displayed in the editor
    [System.Serializable]
    public class ForceTransferredEvent : UnityEvent<ForceTransferredEventData2D> { }

    [SerializeField]
    [Tooltip("Strength of the blowback delivered to the blowback socket")]
    private float strength;
    [SerializeField]
    [Tooltip("Duration for which the force simulated mover is forced to moved")]
    private float time = ForceSimulatedMover2D.DEFAULT_BLOWBACK_TIME;

    [SerializeField]
    [Tooltip("Event invoked when the force is transferred to a force socket")]
    private ForceTransferredEvent _forceTransferredEvent;
    public ForceTransferredEvent forceTransferredEvent { get { return _forceTransferredEvent; } }

    protected override void ProcessComponent(ForceSocket2D socket)
    {
        // Use vector with tail at this and tip at the socket for the direction of the force
        Vector2 toSocket = socket.transform.position - transform.position;
        socket.AbsorbForce(toSocket, strength, time);
        _forceTransferredEvent.Invoke(new ForceTransferredEventData2D(this, socket, toSocket, strength, time));
    }
}
