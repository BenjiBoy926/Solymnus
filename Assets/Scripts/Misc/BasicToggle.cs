using UnityEngine;
using UnityEngine.Events;
using System;

public class BasicToggle : MonoBehaviour
{
    [Serializable]
    public class BoolEvent : UnityEvent<bool> { };

    [SerializeField]
    [Tooltip("Current state of the toggle")]
    private bool state;
    [SerializeField]
    [Tooltip("Event called whenever the toggle is toggled")]
    private BoolEvent _toggleEvent;
    public BoolEvent toggleEvent { get { return _toggleEvent; } }
    [SerializeField]
    [Tooltip("Event called when the toggle toggles to true")]
    private UnityEvent _toggleTrueEvent;
    public UnityEvent toggleTrueEvent { get { return _toggleTrueEvent; } }
    [SerializeField]
    [Tooltip("Event called when the toggle toggles to false")]
    private UnityEvent _toggleFalseEvent;
    public UnityEvent toggleFalseEvent { get { return _toggleFalseEvent; } }

    public void Toggle()
    {
        state = !state;
        _toggleEvent.Invoke(state);
        
        if (state)
        {
            _toggleTrueEvent.Invoke();
        }
        else
        {
            _toggleFalseEvent.Invoke();
        }
    }
}
