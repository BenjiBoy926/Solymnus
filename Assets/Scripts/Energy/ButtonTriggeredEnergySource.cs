using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/*
 * Trigger the given energy source to transfer energy to the given
 * energy socket when the UI button associated with the socket is pressed
 */ 

public class ButtonTriggeredEnergySource : MonoBehaviour
{
    /*
     * Editor data
     */ 
    [SerializeField]
    [Tooltip("Reference to the source of the energy to transfer to the energy socket")]
    private EnergySource source;

    /*
     * Private data
     */ 
    private Dictionary<Button, EnergySocket> buttonSocketPairs;

    /*
     * Public interface
     */ 
    public void AddButtonSocketPair(Button button, EnergySocket socket)
    {
        buttonSocketPairs.Add(button, socket);
    }

    private void UpdateButtonEvents()
    {
        foreach(KeyValuePair<Button, EnergySocket> pair in buttonSocketPairs)
        {

        }
    }
}
