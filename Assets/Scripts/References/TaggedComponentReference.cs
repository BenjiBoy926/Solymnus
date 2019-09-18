using UnityEngine;

public class TaggedComponentReference<TComponent> : ComponentReference<TComponent>
{
    [SerializeField]
    [TagSelector]
    [Tooltip("Tag of the game object to search for the desired component. " +
        "This game object should have a component of the desired type attached to it" +
        " if includeChildren = false, or somewhere in its children if includeChildren = true")]
    private string gameObjectTag;
    [SerializeField]
    [Tooltip("If true, search the children of the game object as well as the object itself. " +
        "Otherwise, search only the object")]
    private bool includeChildren;

    public override TComponent reference
    {
        get
        {
            // If the reference is not null, simply return it
            if (_reference != null)
            {
                return reference;
            }
            // If the reference is null, try to find it on the game object
            else
            {
                GameObject obj = GameObject.FindGameObjectWithTag(gameObjectTag);

                if (obj)
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
                else
                {
                    Debug.LogError("From component reference attached to game object named " + gameObject.name +
                        ". No game object was found with the tag " + gameObjectTag);
                    return default;
                }
            }
        }
    }
}
