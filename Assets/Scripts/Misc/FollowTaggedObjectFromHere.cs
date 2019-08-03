using UnityEngine;
using System.Collections;

// Type of follow object that gets the difference at the start of the scene
// and keeps that distance as it follows the object
public class FollowTaggedObjectFromHere : FollowTaggedObject
{
    private Vector3 fixedDifference;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        fixedDifference = transform.position - taggedTransform.position;
    }

    protected override Vector3 Diff()
    {
        return fixedDifference;
    }
}
