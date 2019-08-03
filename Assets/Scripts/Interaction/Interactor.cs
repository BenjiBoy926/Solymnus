using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    [Serializable]  // So that the variable appears in the editor
    public class InteractableEvent : UnityEvent<Interactable> { };

    /*
     * Editor Fields
     */ 
    [SerializeField]
    [Tooltip("Reference to the script that will add and remove interactables to the list " +
        "when the interactor collides with them")]
    private CollisionInteractableProcessor interactableProcessor;
    [SerializeField]
    [Tooltip("Event invoked when the interactor interacts with the nearest interactable")]
    private InteractableEvent _interactEvent;
    public InteractableEvent interactEvent { get { return _interactEvent; } }
    [SerializeField]
    [Tooltip("Event invoked when the interactor adds an interactable to its internal list")]
    private InteractableEvent _interactableAddedEvent;
    public InteractableEvent interactableAddedEvent { get { return _interactableAddedEvent; } }
    [SerializeField]
    [Tooltip("Event invoked when the interactor removes an interactable from its internal list")]
    private InteractableEvent _interactableRemovedEvent;
    public InteractableEvent interactableRemovedEvent { get { return _interactableRemovedEvent; } }

    /*
     * Private fields
     */

    // List of all interactable objects in range
    public List<Interactable> interactablesInRange
    {
        get;
        private set;
    } = new List<Interactable>();

    /*
     * Public interface
     */ 

    public void Interact()
    {
        Interactable interactable = NextInteractable();

        // If an interactable was selected, interact with it
        if (interactable != null)
        {
            // If interactable is persistent, rotate the list
            if (interactable.persistent)
            {
                interactablesInRange.RotateLeft();
            }
            // If not, remove the interactable completely
            else
            {
                RemoveInteractable(0);
            }

            interactable.Interact(this);
            _interactEvent.Invoke(interactable);
        }
    }

    // Return the interactable that the interactor will interact with 
    // on the next call to "Interact()"
    public Interactable NextInteractable()
    {
        if (interactablesInRange.Count > 0)
        {
            return interactablesInRange[0];
        }
        else
        {
            return null;
        }
    }

    /*
     * Private helpers
     */ 

    private void Start()
    {
        interactableProcessor.interactableEnterEvent.AddListener(AddInteractable);
        interactableProcessor.interactableExitEvent.AddListener(RemoveInteractable);
    }
    private void AddInteractable(Interactable interactable)
    {
        interactablesInRange.Add(interactable);
        _interactableAddedEvent.Invoke(interactable);
    }
    private void RemoveInteractable(Interactable interactable)
    {
        interactablesInRange.Remove(interactable);
        _interactableRemovedEvent.Invoke(interactable);
    }
    private void RemoveInteractable(int index)
    {
        Interactable removed = interactablesInRange[0];
        interactablesInRange.RemoveAt(0);
        _interactableRemovedEvent.Invoke(removed);
    }
}
