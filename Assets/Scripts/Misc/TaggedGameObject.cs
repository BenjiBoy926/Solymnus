using UnityEngine;
using System;

[Serializable]
public class TaggedGameObject
{
    [SerializeField]
    [Tooltip("Tag of the game object to store a reference to")]
    private string tag;

    // Reference to the game object with the given tag
    private GameObject _gameObject = null;
    public GameObject gameObject
    {
        get
        {
            if (_gameObject == null)
            {
                _gameObject = GameObject.FindGameObjectWithTag(tag);
            }
            return _gameObject;
        }
    }
}
