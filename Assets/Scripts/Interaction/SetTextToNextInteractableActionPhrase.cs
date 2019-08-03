using UnityEngine;
using UnityEngine.UI;

public class SetTextToNextInteractableActionPhrase : MonoBehaviour
{
    /*
     * Editor fields
     */ 

    [SerializeField]
    [Tooltip("Tag of the game object that has a text component")]
    private TaggedGameObject taggedObject;
    [SerializeField]
    [Tooltip("The script that interacts with iteractable objects")]
    private Interactor interactor;

    /*
     * Private fields
     */ 

    // Reference to the text object
    private Text text;

    private void Start()
    {
        text = taggedObject.gameObject.GetComponentInChildren<Text>();
        interactor.interactEvent.AddListener(UpdateText);
        interactor.interactableAddedEvent.AddListener(UpdateText);
        interactor.interactableRemovedEvent.AddListener(UpdateText);
    }

    // Each time interactables are added or removed from the list, update the text
    private void UpdateText(Interactable interactable)
    {
        Interactable nextUp = interactor.NextInteractable();

        if (nextUp != null)
        {
            text.text = nextUp.actionPhrase;
        }
    }
}
