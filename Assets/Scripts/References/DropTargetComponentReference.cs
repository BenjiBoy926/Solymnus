using UnityEngine;

public class DropTargetComponentReference<TComponent> : ComponentReference<TComponent>
{
    [SerializeField]
    [Tooltip("This game object should have a component of the desired type attached to it" +
        " if includeChildren = false, or somewhere in its children if includeChildren = true")]
    private GameObject obj;
    [SerializeField]
    [Tooltip("If true, the game object given and all of its children are searched for the component. " +
        "Otherwise, only the object itself is searched for the component.")]
    private bool includeChildren;

    public override TComponent reference
    {
        get
        {
            // If the reference is non-null, simply return it
            if (_reference != null)
            {
                return _reference;
            }
            // If the reference is null, try to get it from the game object
            else
            {
                _reference = obj.GetComponent<TComponent>(includeChildren);
                if (_reference != null)
                {
                    return _reference;
                }
                else
                {
                    PrintComponentNotFoundErrorMessage(obj.name, includeChildren);
                    return default;
                }
            }
        }
    }
}
