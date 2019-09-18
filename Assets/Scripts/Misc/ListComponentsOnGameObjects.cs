using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ListComponentsOnGameObjects<TComponent> : MonoBehaviour
{
    [SerializeField]
    [TagSelector]
    [Tooltip("Tag of the game objects to check for the given components")]
    private string gameObjectTag;
    [SerializeField]
    [Tooltip("If true, search the game object and its children for the desired component")]
    private bool includeChildren;
    [SerializeField]
    [Tooltip("Event invoked when the component list is updated and the values in the list " +
        "are different after the update")]
    private UnityEvent _listChangedEvent;
    public UnityEvent listChangedEvent { get { return _listChangedEvent; } }

    // List of components on the game objects with the given tag
    public List<TComponent> components { get; private set; } = new List<TComponent>();

    public void UpdateComponentList()
    {
        // Find all game objects with the tag
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(gameObjectTag);
        TComponent component;
        // Create a copy of the list before modifying it
        List<TComponent> oldComponentList = new List<TComponent>(components);

        // Clear out the list
        components.Clear();

        // For each game object, add its component to the list
        foreach (GameObject obj in gameObjects)
        {
            component = obj.GetComponent<TComponent>(includeChildren);
            if (component != null)
            {
                components.Add(component);
            }
        }

        if (!oldComponentList.Equals(components))
        {
            _listChangedEvent.Invoke();
        }
    }
}
