using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EmissionEvent : UnityEvent<Vector3> { };

public interface IEmitter : IConsumer<Vector3>
{
    void Emit(Vector3 aim);
    EmissionEvent emissionEvent { get; }
}
