using UnityEngine;
using System;

/*
 * CLASS ConstrainedEmitter2D
 * --------------------------
 * A type of emitter with a list of function pointers that return
 * true-false. The emitter only emits if all return true
 * --------------------------
 */

public class ConstrainedEmitter : MonoBehaviour, IEmitter, IConstrainable
{
    /*
     * Editor data 
     */

    [SerializeField]
    [Tooltip("Reference to a game object with a component of type IEmitter2D")]
    private EmitterComponent emitter;
    [SerializeField]
    [Tooltip("Event invoked when the emitter emits")]
    private EmissionEvent _emissionEvent;
    public EmissionEvent emissionEvent { get { return _emissionEvent; } }

    /*
     * Private data
     */

    public BooleanFunctorList constraints { get; } = new BooleanFunctorList();

    /*
     * PUBLIC INTERFACE
     */

    public void AddConstraint(Func<bool> constraint)
    {
        constraints.Add(constraint);
    }

    // Emit only if constraints return true
    public void Emit(Vector3 aimVector)
    {
        if (constraints.result)
        {
            ForceEmit(aimVector);
        }
    }
    public void Consume(Vector3 aimVector)
    {
        Emit(aimVector);
    }
    // Optionally force the emitter to emit, ignoring the constraint
    public void ForceEmit(Vector3 aimVector)
    {
        emitter.component.Emit(aimVector);
        _emissionEvent.Invoke(aimVector);
    }
}
