using UnityEngine;
using System;

public abstract class CollisionComponentProcessor<TComponent> : MonoBehaviour
{
    // COLLISION ENTER
    private void OnCollisionEnter(Collision collision)
    {
        TryProcessComponent(collision.gameObject, ProcessCollisionEnter);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryProcessComponent(collision.gameObject, ProcessCollisionEnter);
    }
    private void OnTriggerEnter(Collider other)
    {
        TryProcessComponent(other.gameObject, ProcessCollisionEnter);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryProcessComponent(collision.gameObject, ProcessCollisionEnter);
    }

    // COLLISION STAY
    private void OnCollisionStay(Collision collision)
    {
        TryProcessComponent(collision.gameObject, ProcessCollisionStay);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        TryProcessComponent(collision.gameObject, ProcessCollisionStay);
    }
    private void OnTriggerStay(Collider other)
    {
        TryProcessComponent(other.gameObject, ProcessCollisionStay);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        TryProcessComponent(collision.gameObject, ProcessCollisionStay);
    }

    // COLLISION EXIT
    private void OnCollisionExit(Collision collision)
    {
        TryProcessComponent(collision.gameObject, ProcessCollisionExit);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        TryProcessComponent(collision.gameObject, ProcessCollisionExit);
    }
    private void OnTriggerExit(Collider other)
    {
        TryProcessComponent(other.gameObject, ProcessCollisionExit);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        TryProcessComponent(collision.gameObject, ProcessCollisionExit);
    }

    // Try to get the component off of the object, then invoke the specified event with it
    private void TryProcessComponent(GameObject obj, Action<TComponent> action)
    {
        TComponent component = obj.GetComponent<TComponent>();
        if (component != null)
        {
            try
            {
                action.Invoke(component);
            }
            catch (MissingComponentException) { }
        }
    }

    // Processes overridden in base classes
    protected abstract void ProcessCollisionEnter(TComponent component);
    protected abstract void ProcessCollisionStay(TComponent component);
    protected abstract void ProcessCollisionExit(TComponent component);
}
