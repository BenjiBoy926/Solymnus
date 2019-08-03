using UnityEngine;

public abstract class FollowTaggedObject : MonoBehaviour
{
    /*
     * Editor data
     */ 

    [SerializeField]
    [Tooltip("Reference to the object to follow")]
    private TaggedGameObject taggedObject;

    /*
     * Private data
     */

    protected Transform taggedTransform;

    protected virtual void Start()
    {
        taggedTransform = taggedObject.gameObject.transform;
    }
    void Update()
    {
        transform.position = taggedTransform.position + Diff();
    }

    // Get the difference to add to the transform being followed
    // as specified in base classes
    protected abstract Vector3 Diff();
}
