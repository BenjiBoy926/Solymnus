using UnityEngine;
using UnityEngine.UI;

public class SetupInteractionVirtualButton : MonoBehaviour
{
    /*
     * Editor fields
     */ 

    [SerializeField]
    [Tooltip("Tag on the virtual button game object")]
    private TaggedGameObject virtualButtonObject;
    [SerializeField]
    [Tooltip("If true, the button is always active. Otherwise, the button " +
        "is only active while there are interactables in range of the interactor")]
    private bool persistent;
    [SerializeField]
    [Tooltip("Reference to the object that interacts with interactable objects")]
    private Interactor interactor;

    /*
     * Private data
     */ 

    // The text on the virtual button
    private Text buttonText;

    // Use this for initialization
    void Start()
    {
        buttonText = virtualButtonObject.gameObject.GetComponentInChildren<Text>();
        interactor.interactableAddedEvent.AddListener(UpdateButton);
        interactor.interactableRemovedEvent.AddListener(UpdateButton);
    }

    private void UpdateButton(Interactable interactable)
    {
        Interactable nextUp = interactor.NextInteractable();

        if (nextUp != null)
        {
            buttonText.text = nextUp.actionPhrase;
        }

        // Set the button to be active if interactable objects are in range
        virtualButtonObject.gameObject.SetActive(nextUp != null);
    }
}
