using UnityEngine;

/*
 * A simple pair type that also inherits from MonoBehaviour
 */ 
public abstract class MonoPair<TFirst, TSecond> : MonoBehaviour
{
    public abstract TFirst first { get; }
    public abstract TSecond second { get; }
}
