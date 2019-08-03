using UnityEngine;
using System.Collections;

public class SupplyVector2PlaneTo3Plane : SupplyVector2To3
{
    [SerializeField]
    [Tooltip("Normal to the 3D plane that the 2D vector will be translated into")]
    protected Vector3 planeNormal;

    public override Vector3 Supply()
    {
        return supplier.component.Supply().MapToPlane(planeNormal);
    }
}
