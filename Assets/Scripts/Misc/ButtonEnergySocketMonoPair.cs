using UnityEngine;
using UnityEngine.UI;

public class ButtonEnergySocketMonoPair : MonoPair<Button, EnergySocket>
{
    [SerializeField]
    [Tooltip("Button paired up with the energy socket")]
    private Button button;
    [SerializeField]
    [Tooltip("Energy socket paired up with the button")]
    private EnergySocket socket;

    public override Button first
    {
        get
        {
            return button;
        }
    }
    public override EnergySocket second
    {
        get
        {
            return socket;
        }
    }
}
