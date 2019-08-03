using UnityEngine;

public abstract class EmitterConstraint2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Emitter to apply a constraint to")]
    protected ConstrainedEmitter2D emitter;

    // Use this for initialization
    protected virtual void Start()
    {
        emitter.AddConstraint(Constraint);
    }

    protected abstract bool Constraint();
}
