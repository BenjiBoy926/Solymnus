using UnityEngine;

public class CollisionTriggeredEnergySource : CollisionEnterComponentProcessor<EnergySocket>
{
    [SerializeField]
    [Tooltip("The source of energy that transfers energy " +
        "to any energy socket it comes into contact with")]
    private EnergySource source;

    protected override void ProcessComponent(EnergySocket component)
    {
        component.AbsorbEnergy(source);
    }
}
