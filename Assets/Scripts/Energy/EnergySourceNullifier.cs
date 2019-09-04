using UnityEngine.Events;

// Simple attribute that disables any energy source it comes into contact with
public class EnergySourceNullifier : CollisionEnterComponentProcessor<EnergySource>
{
    public event UnityAction<EnergyNullifiedEventData> energyNullifiedEvent;

    protected override void ProcessComponent(EnergySource source)
    {
        source.gameObject.SetActive(false);
        energyNullifiedEvent?.Invoke(new EnergyNullifiedEventData(this, source));
    }
}
