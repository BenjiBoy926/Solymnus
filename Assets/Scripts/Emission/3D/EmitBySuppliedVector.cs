using UnityEngine;

public class EmitBySuppliedVector : MonoBehaviour
{
    [System.Serializable]
    public class Vector3Supplier : PolymorphicComponent<ISupplier<Vector3>> { };

    [SerializeField]
    [Tooltip("Reference to a game object with a component of type ISupplier<Vector3>")]
    private Vector3Supplier supplier;
    [SerializeField]
    [Tooltip("Reference to a game object with a component of type IEmitter")]
    private EmitterComponent emitter;

    private void Update()
    {
        Vector3 supply = supplier.component.Supply();
        if (supply != Vector3.zero)
        {
            emitter.component.Emit(supply);
        }
    }
}
