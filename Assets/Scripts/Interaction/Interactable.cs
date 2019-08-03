using UnityEngine;
using UnityEngine.Events;
using System;

public class Interactable : MonoBehaviour
{
    [Serializable]  // So that the variable appears in the editor
    public class InteractorEvent : UnityEvent<Interactor> { };

    [SerializeField]
    [Tooltip("Breif action phrase to describe what the interactor " +
        "will do with this interactable object")]
    private string _actionPhrase;
    public string actionPhrase
    {
        get { return _actionPhrase; }
        set { _actionPhrase = value; }
    }
    [SerializeField]
    [Tooltip("If true, allow multiple interactions with this object while an interactor is present." +
        " Otherwise, only allow one interaction")]
    private bool _persistent;
    public bool persistent { get { return _persistent; } }
    [SerializeField]
    [Tooltip("Event invoked when the an interator interacts with this interactable")]
    private InteractorEvent _interactEvent;
    public InteractorEvent interactEvent { get { return _interactEvent; } }

    public virtual void Interact(Interactor interactor)
    {
        _interactEvent.Invoke(interactor);
    }
}
