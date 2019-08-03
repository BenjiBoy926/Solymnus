using UnityEngine;

/*
 * CLASS SupplyCameraSensitiveMotionVectors
 * ----------------------------------------
 * The final supplier in the chain of Vector2 -> Vector3 suppliers
 * This takes the result from the previous supply, transformed
 * into a 3D plane, and transforms it again such that it is aligned
 * with the projection of the forward vector of the given transform component
 * ----------------------------------------
 */
public class SupplyCameraSensitiveVectors : SupplyVector2PlaneTo3Plane
{
    [SerializeField]
    [Tooltip("Tag on the camera used to transform the vectors")]
    private string cameraTag = "MainCamera";

    // Transform component on the game object with the given tag 
    // (it doesn't really have to be a camera at all)
    private Transform _cameraTransform;
    private Transform cameraTransform
    {
        get
        {
            if (_cameraTransform == null)
            {
                _cameraTransform = GameObject.FindGameObjectWithTag(cameraTag).transform;
            }
            return _cameraTransform;
        }
    }

    // Supply the same vector one level above, transformed by the transformation 
    // between the absolute forward and the camera forward
    public override Vector3 Supply()
    {
        Vector3 absoluteForward = Vector2.up.MapToPlane(planeNormal);
        Vector3 relativeForward = Vector3.ProjectOnPlane(cameraTransform.forward, planeNormal);
        return base.Supply().Transform(absoluteForward, relativeForward);
    }
}
