using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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
    private Dictionary<Button, List<EnergySocket>> buttonSocketPairs = new Dictionary<Button, List<EnergySocket>>();

    /*
     * Public interface
     */ 
    public void AddButtonSocketPair(Button button, EnergySocket socket)
    {
        if (!buttonSocketPairs.ContainsKey(button))
        {
            buttonSocketPairs.Add(button, new List<EnergySocket>());
            button.onClick.AddListener(TransferEnergyCallback(button));
        }
        AddSingleButtonSocketPair(button, socket);
    }

    public void RemoveButtonSocketPair(Button button)
    {
        buttonSocketPairs.Remove(button);
        button.onClick.RemoveListener(TransferEnergyCallback(button));
    }

    public void ClearButtonSocketPairs()
    {
        foreach(KeyValuePair<Button, List<EnergySocket>> pair in buttonSocketPairs)
        {
            pair.Key.onClick.RemoveListener(TransferEnergyCallback(pair.Key));
        }
        buttonSocketPairs.Clear();
    }

    /*
     * Private helpers
     */

    private UnityAction TransferEnergyCallback(Button button)
    {
        return () => TransferEnergy(button);
    }

    // Transfer energy to each energy socket associated with the given button
    private void TransferEnergy(Button button)
    {
        List<EnergySocket> targetSockets;
        if (buttonSocketPairs.TryGetValue(button, out targetSockets))
        {
            foreach(EnergySocket socket in targetSockets)
            {
                source.TransferEnergy(socket);
            }
        }
    }

    // Helper function makes sure there are no duplicate sockets in the list
    // of sockets associated with each button
    private void AddSingleButtonSocketPair(Button button, EnergySocket socket)
    {
        List<EnergySocket> sockets = buttonSocketPairs[button];
        if (!sockets.Contains(socket))
        {
            sockets.Add(socket);
        }
    }
}
