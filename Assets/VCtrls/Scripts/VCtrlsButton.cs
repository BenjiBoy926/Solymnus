using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class VCtrlsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressed = false; // True while the button is pressed
    private bool pressedThisFrame = false;
    private bool releasedThisFrame = false;

    /*
     * Public interface
     */ 

    // Interface implementation
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
        pressedThisFrame = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
        releasedThisFrame = true;
    }

    // Accessors to the button's state
    public bool GetButton()
    {
        return isPressed;
    }
    public bool GetButton(InputButtonType type)
    {
        switch(type)
        {
            case InputButtonType.Down:  return GetButtonDown();
            case InputButtonType.Stay:  return GetButton();
            case InputButtonType.Up:    return GetButtonUp();
            default:    return false;
        }
    }
    public bool GetButtonDown()
    {
        return pressedThisFrame;
    }
    public bool GetButtonUp()
    {
        return releasedThisFrame;
    }

    /*
     * Private helpers
     */ 

    private void Update()
    {
        // This appears to work, but I'm suspicious because it's not clear to me why...
        pressedThisFrame = false;
        releasedThisFrame = false;
    }

    private void OnEnable()
    {
        FalsifyAllState();
    }
    private void OnDisable()
    {
        FalsifyAllState();
    }

    private void FalsifyAllState()
    {
        isPressed = false;
        pressedThisFrame = false;
        releasedThisFrame = false;
    }
}
