using UnityEngine;
using UnityEngine.Events;
using System;

public class CollisionInteractableProcessor : CollisionComponentProcessor<Interactable>
{
    [Serializable]  // So that the variables appear in the editor
    public class InteractableEvent : UnityEvent<Interactable> { }

    [SerializeField]
    [Tooltip("Event invoked when the gameobject collides with another object " +
        "with an interactable component")]
    private InteractableEvent _interactableEnterEvent;

    [SerializeField]
    [Tooltip("Event invoked each frame the gameobject is colliding with another object " +
        "with an interactable component")]
    private InteractableEvent _interactableStayEvent;

    [SerializeField]
    [Tooltip("Event invoked when the gameobject exits the collider of another object " +
        "with an interactable component")]
    private InteractableEvent _interactableExitEvent;

    public InteractableEvent interactableEnterEvent { get { return _interactableEnterEvent; } }
    public InteractableEvent interactableStayEvent { get { return _interactableStayEvent; } }
    public InteractableEvent interactableExitEvent { get { return _interactableExitEvent; } }

    protected override void ProcessCollisionEnter(Interactable component)
    {
        _interactableEnterEvent.Invoke(component);
    }
    protected override void ProcessCollisionStay(Interactable component)
    {
        _interactableStayEvent.Invoke(component);
    }
    protected override void ProcessCollisionExit(Interactable component)
    {
        _interactableExitEvent.Invoke(component);
    }
}
