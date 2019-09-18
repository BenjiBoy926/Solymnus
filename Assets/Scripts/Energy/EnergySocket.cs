using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/*
 * CLASS EnergySocket
 * ------------------
 * An object that can absorb energy. Adds an interpretive layer
 * on top of the energy and decides how much energy is actually absorbed.
 * This allows energies to only identify themselves, while sockets
 * decide how they will respond to different types of energies
 * ------------------
 */ 

public class EnergySocket : MonoBehaviour
{
    /*
     * PUBLIC TYPEDEFS
     */
    [System.Serializable] public class EnergyEvent : UnityEvent<EnergyEventData> { };

    /*
     * PUBLIC DATA
     */ 

    [SerializeField]
    [Tooltip("Energy sources with any of these tags cause the socket to absorb negative energy")]
    private List<Tag> hazards;
    [SerializeField]
    [Tooltip("Energy sources with any of these tags cause the socket to absorb positive energy")]
    private List<Tag> healers;
    [SerializeField]
    [Tooltip("Applies a multiplier to the energy absorbed by the given energy type")]
    private List<EnergyIntakeInfo> intakeInfo;
    [SerializeField]
    [Tooltip("Set of events invoked when the socket absorbs energy")]
    private EnergyEvent _energyAbsorbedEvent;
    public EnergyEvent energyAbsorbedEvent { get { return _energyAbsorbedEvent; } }

    // Process the energy and raise the event
    public int AbsorbEnergy(EnergySource source)
    {
        int energyAbsorbed = ProcessEnergy(source.energy);
        _energyAbsorbedEvent.Invoke(new EnergyEventData(source, this, energyAbsorbed));
        return energyAbsorbed;
    }
    // Determine how much energy is absorbed by the socket
    // based on its local fields
    private int ProcessEnergy(Energy energy)
    {
        int processedPower = 0; // Power of the energy when processed by the socket's fields

        // Check to see if the energy is hazardous or healthy
        bool hazardous = hazards.Contains(energy.tag);
        bool healing = healers.Contains(energy.tag);

        if (hazardous || healing)
        {
            // Find an intake info whose type matches the source type
            EnergyIntakeInfo matchedInfo = intakeInfo.Find(x => x.type == energy.type);
            // If the socket does not have intake info specified for energy of this type...
            if (matchedInfo == null)
            {
                //...assign unmodified energy power
                processedPower = energy.power;
            }
            // If the socket has intake info specified for energy of this type...
            else
            {
                //...multiply energy power by intake info multiplier
                processedPower = Mathf.RoundToInt(energy.power * matchedInfo.multiplier);
            }
            // Force power to negative if the energy is classified as hazardous
            processedPower = Mathf.Abs(processedPower);
            if (hazardous)
            {
                processedPower *= -1;
            }
        }

        return processedPower;
    }

    // Set the multiplier for the given energy type
    // If no intake info exists with the given type, add one to the list
    public void SetIntakeMultiplier(EnergyType energyType, float multiplier)
    {
        EnergyIntakeInfo info = intakeInfo.Find(x => x.type == energyType);
        if (info != null)
        {
            info.multiplier = multiplier;
        }
        else
        {
            intakeInfo.Add(new EnergyIntakeInfo(energyType, multiplier));
        }
    }
}
