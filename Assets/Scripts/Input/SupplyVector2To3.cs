using UnityEngine;
using System.Collections;

// An abstract class that supplies a 3D vector based on a 2D vector
public abstract class SupplyVector2To3 : MonoBehaviour, ISupplier<Vector3>
{
    // So that variable appears in the editor
    [System.Serializable]
    public class VectorSupplier : PolymorphicComponent<ISupplier<Vector2>> { }

    [SerializeField]
    [Tooltip("Reference to the script that supplies the 2D vector")]
    protected VectorSupplier supplier;

    public abstract Vector3 Supply();
}
