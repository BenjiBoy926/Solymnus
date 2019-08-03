using UnityEngine;
using System;

public class InteractBySuppliedBool : MonoBehaviour
{
    [Serializable]  // So that the variable appears in the editor
    public class BoolSupplier : PolymorphicComponent<ISupplier<bool>> { };

    [SerializeField]
    [Tooltip("Reference to a gameobject with a script that supplies boolean values")]
    private BoolSupplier supplier;
    [SerializeField]
    [Tooltip("Reference to the interactor that triggers the interation")]
    private Interactor interactor;

    private void Update()
    {
        if (supplier.component.Supply())
        {
            interactor.Interact();
        }
    }
}
