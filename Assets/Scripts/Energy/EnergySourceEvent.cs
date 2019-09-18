using UnityEngine;

public class EnergySourceEvent : TestedEnergyEvent
{
    /*
     * PUBLIC DATA
     */ 

    [SerializeField]
    [Tooltip("Energy source with the tested event")]
    private EnergySource source;

    // Use this for initialization
    void Start()
    {
        source.energyTransferredEvent.AddListener(TestEnergyEvent);
    }
}
