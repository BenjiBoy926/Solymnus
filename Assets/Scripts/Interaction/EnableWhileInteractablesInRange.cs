using UnityEngine;

public class EnableWhileInteractablesInRange : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tag on the game object to enable while interactables are in range")]
    private TaggedGameObject taggedObject;
    [SerializeField]
    [Tooltip("The interactor to check for interactables in range")]
    private Interactor interactor;

    // Use this for initialization
    void Start()
    {
        interactor.interactableAddedEvent.AddListener(CheckInteractables);
        interactor.interactableRemovedEvent.AddListener(CheckInteractables);
        CheckInteractables(null);
    }

    // Update whether the object is active or inactive
    // based on interactor's interactables in range
    private void CheckInteractables(Interactable interactable)
    {
        taggedObject.gameObject.SetActive(interactor.interactablesInRange.Count > 0);
    }
}
