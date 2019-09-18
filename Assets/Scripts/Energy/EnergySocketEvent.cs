using UnityEngine;

public class EnergySocketEvent : TestedEnergyEvent
{
    [SerializeField]
    [Tooltip("Energy socket that triggers the energy test")]
    private EnergySocket socket;

    // Use this for initialization
    void Start()
    {
        socket.energyAbsorbedEvent.AddListener(TestEnergyEvent);
    }
}
