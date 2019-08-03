using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS SupplyCombiner
 * --------------------
 * Combines multiple resource suppliers by supplying the value of the first
 * in the list that does not equal the default value of the resource,
 * or supplies the default value if all suppliers are supplying the default value
 * --------------------
 */ 

public class SupplyCombiner<TResource> : MonoBehaviour, ISupplier<TResource>
{
    /*
     * Editor data
     */ 
    [SerializeField]
    [Tooltip("Game objects with ISupplier scripts attached to them")]
    private List<GameObject> supplierObjects;

    /*
     * Private data
     */ 

    // List of supplying scripts on the game objects
    private List<ISupplier<TResource>> _suppliers;
    private List<ISupplier<TResource>> suppliers
    {
        get
        {
            if (_suppliers == null)
            {
                InitializeSuppliers();
            }
            return _suppliers;
        }
    }

    /*
     * Public interface
     */ 

    public TResource Supply()
    {
        TResource resource;

        foreach(ISupplier<TResource> supply in suppliers)
        {
            resource = supply.Supply();

            // If the supply is not the default, supply it
            if (!resource.Equals(default(TResource)))
            {
                return resource;
            }
        }

        // If we get to this point, all values are default,
        // so supply default value
        return default;
    }

    /*
     * Private helpers
     */ 

    private void InitializeSuppliers()
    {
        ISupplier<TResource> supply;    // Supplier on the current game object

        // Instantiate the list
        _suppliers = new List<ISupplier<TResource>>();

        // Try to get a supplier component from all game objects
        foreach (GameObject obj in supplierObjects)
        {
            supply = obj.GetComponent<ISupplier<TResource>>();
            if (supply != null)
            {
                _suppliers.Add(supply);
            }
        }
    }
}
