using UnityEngine;

public abstract class ComponentReference<TComponent> : MonoBehaviour
{
    protected TComponent _reference = default;
    public abstract TComponent reference { get; }

    protected void PrintComponentNotFoundErrorMessage(string gameObjectName, bool includeChildren)
    {
        if (includeChildren)
        {
            Debug.LogError("From component reference attached to game object named " + gameObject.name + 
                ". No object of type " + typeof(TComponent).ToString() +
                " attached to GameObject with name " + gameObjectName +
                " or any of its children");
        }
        else
        {
            Debug.LogError("From component reference attached to game object named " + gameObject.name + 
                ". No object of type " + typeof(TComponent).ToString() +
                " attached to GameObject with name " + gameObjectName);
        }

    }
}