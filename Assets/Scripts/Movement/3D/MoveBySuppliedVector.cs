using UnityEngine;
using System.Collections;

public class MoveBySuppliedVector : MonoBehaviour
{
    // So that the variable can appear in the editor
    [System.Serializable]
    public class VectorSupplier : PolymorphicComponent<ISupplier<Vector3>> { }

    [SerializeField]
    [Tooltip("Reference to the script that causes the movement")]
    private KinematicMoverController mover;
    [SerializeField]
    [Tooltip("Reference to the game object with a script that will supply the movement vector")]
    private VectorSupplier supplier;

    private void Update()
    {
        mover.Move(supplier.component.Supply());
    }
}
