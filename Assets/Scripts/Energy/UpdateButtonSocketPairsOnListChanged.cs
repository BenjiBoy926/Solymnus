using UnityEngine;

/*
 * Update the button socket pairs on a button triggered energy source
 * based on the button socket pairs in a component listing object
 */ 
public class UpdateButtonSocketPairsOnListChanged : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Script with the button socket pairs that must be updated")]
    private ButtonTriggeredEnergySource source;
    [SerializeField]
    [Tooltip("Reference to the script that gets the list of button and energy socket pairs")]
    private ListButtonEnergySocketMonoPairsOnGameObjects pairsList;

    private void Start()
    {
        pairsList.listChangedEvent.AddListener(UpdateButtonSocketPairs);
    }

    private void UpdateButtonSocketPairs()
    {
        source.ClearButtonSocketPairs();
        foreach(ButtonEnergySocketMonoPair pair in pairsList.components)
        {
            source.AddButtonSocketPair(pair.first, pair.second);
        }
    }
}
