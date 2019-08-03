using UnityEngine;
using System.Collections;

public class SupplyInputAxisVector : MonoBehaviour, ISupplier<Vector2>
{
    [SerializeField]
    [Tooltip("Name of the horizontal axis in the input manager")]
    private string horizontalAxisName = "Horizontal";
    [SerializeField]
    [Tooltip("Name of the vertical axis in the input manager")]
    private string verticalAxisName = "Vertical";
    [SerializeField]
    [Tooltip("If true, give discrete values -1, 0, 1. If false, " +
        "smoothly interpolate the values between -1 and 1")]
    private bool raw;

    public Vector2 Supply()
    {
        if(raw)
        {
            return new Vector2(Input.GetAxisRaw(horizontalAxisName), Input.GetAxisRaw(verticalAxisName));
        }
        else
        {
            return new Vector2(Input.GetAxis(horizontalAxisName), Input.GetAxis(verticalAxisName));
        }
    }
}
