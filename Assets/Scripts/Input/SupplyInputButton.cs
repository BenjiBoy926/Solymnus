using UnityEngine;

public class SupplyInputButton : MonoBehaviour, ISupplier<bool>
{
    [SerializeField]
    [Tooltip("Name of the button in the input manager")]
    private string buttonName;
    [SerializeField]
    [Tooltip("Register true once when the button is pressed (down)," +
        " once each frame the button is pressed (stay), or once" +
        " when the button is released (up)")]
    private InputButtonType type;

    public bool Supply()
    {
        return InputExt.GetButton(buttonName, type);
    }
}
