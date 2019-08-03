using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class EmissionEvent2D : UnityEvent<Vector2> { };

public interface IEmitter2D : IConsumer<Vector2>
{
    void Emit(Vector2 aim);
    EmissionEvent2D emissionEvent { get; }
}
