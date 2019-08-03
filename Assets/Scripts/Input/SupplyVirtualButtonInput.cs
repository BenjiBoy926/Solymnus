using UnityEngine;
using System.Collections.Generic;

public class SupplyVirtualButtonInput : MonoBehaviour, ISupplier<bool>
{
    [SerializeField]
    [Tooltip("Tag of the button in the VCtrlsManager")]
    private string buttonTag;
    [SerializeField]
    [Tooltip("Supply true once on press (Down), once on release (Up), or " +
        "once each frame the button is held (Stay)")]
    private InputButtonType type;

    public bool Supply()
    {
        try
        {
            return VCtrlsManager.Button[buttonTag].GetButton(type);
        }
        catch(KeyNotFoundException)
        {
            Debug.LogError("Virtual button input supplier on game object " + gameObject.name +
                " could not find virtual button with tag " + buttonTag);
            return false;
        }
    }
}
