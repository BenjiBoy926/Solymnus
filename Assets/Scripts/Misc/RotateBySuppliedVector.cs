using UnityEngine;

public class RotateBySuppliedVector : MonoBehaviour
{
    // So that the variable appears in the editor
    [System.Serializable]
    public class SupplierComponent : PolymorphicComponent<ISupplier<Vector3>> { };

    /*
     * Editor data
     */

    [SerializeField]
    [Tooltip("Reference to the game object with a component of type ISupplier<Vector3>")]
    private SupplierComponent supplier;
    [SerializeField]
    [Tooltip("Transform component to rotate towards the supplied vector")]
    private Transform trans;
    [SerializeField]
    [Tooltip("Vector that represents up for this transform component")]
    private Vector3 worldUp = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        Vector3 supply = supplier.component.Supply();
        if (supply != Vector3.zero)
        {
            trans.LookAt(trans.position + supplier.component.Supply(), worldUp);
        }
    }
}
