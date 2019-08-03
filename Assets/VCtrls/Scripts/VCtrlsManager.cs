using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum Constraints
{
    None,
    Horizontal,
    Vertical
}

[AddComponentMenu("Event/Virtual Controls Manager")]
public class VCtrlsManager : MonoBehaviour
{
    // Enforce Singleton pattern for the manager
    private static VCtrlsManager _instance;
    public static VCtrlsManager instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<VCtrlsManager>();
            return _instance;
        }
    }

    // Dictionaries map the object to the tag of the gameobject it's attached to
    public static Dictionary<string, VCtrlsButton> Button = new Dictionary<string, VCtrlsButton>();
    public static Dictionary<string, VCtrlsJoystick> Stick = new Dictionary<string, VCtrlsJoystick>();

    void OnEnable()
    {
        OnValidate();
    }

    // Register all joysticks and virtual buttons
    void OnValidate()
    {
        RegisterObjectsOfType(Stick);
        RegisterObjectsOfType(Button);
    }
    // Add all objects in the scene to the dictionary, referenced by their game object's tags
    void RegisterObjectsOfType<Type>(Dictionary<string, Type> dictionary) where Type : Component
    {
        Type[] objects = FindObjectsOfType<Type>();
        dictionary.Clear();
        foreach (Type i in objects)
        {
            try
            {
                dictionary.Add(i.gameObject.tag, i);
            }
            catch (System.ArgumentException)
            {
                Debug.LogError("Multiple Virtual Controls exist with the conflicting tag of [" + i.gameObject.tag +
                    "] Please consider renaming your virtual controls to all have unique names.");
            }
        }
    }
}
