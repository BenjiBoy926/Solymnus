using System.Collections.Generic;
using UnityEngine;

public class SupplyVirtualJoystickInput : MonoBehaviour, ISupplier<Vector2>
{
    [SerializeField]
    [Tooltip("Name of the joystick root game object tag to obtain input from")]
    private string joystickTag;

    public Vector2 Supply()
    {
        try
        {
            VCtrlsJoystick joystick = VCtrlsManager.Stick[joystickTag];
            return new Vector2(joystick.GetAxis(0), joystick.GetAxis(1));
        }
        catch(KeyNotFoundException)
        {
            Debug.LogError("Virtual joystick input supplier could not find a joystick with tag " + joystickTag);
            return Vector2.zero;
        }
    }
}
