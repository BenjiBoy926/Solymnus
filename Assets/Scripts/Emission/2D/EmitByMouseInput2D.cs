﻿using UnityEngine;
using System.Collections;

/*
 * CLASS EmitByMouseInput2D
 * ------------------------
 * Causes a constrained emitter to emit towards the mouse position
 * when the mouse button is pressed
 * ------------------------
 */

public class EmitByMouseInput2D : MonoBehaviour
{
    /*
     * PUBLIC DATA
     */ 

    [SerializeField]
    [Tooltip("Reference to the game object with the emitter on it. *** REQUIREMENT *** Component of type \"IEmitter\"")]
    private GameObject emitterObj;
    [SerializeField]
    private string emitButtonName;  // Name of the button in the input manager that the user presses to emit an object
    [SerializeField]
    [Tooltip("Defines how the input is triggerd: once when the button is pressed, once when released, or once each frame held")]
    private InputButtonType buttonType;

    /*
     * PRIVATE DATA
     */

    private IEmitter2D emitter;   // Scrit on the emitter object that implements IEmitter
    private Camera mainCamera;  // Stored reference to the main camera
    private Vector2 mousePosition;  // Position of the mouse in world space

    /*
     * PRIVATE HELPERS
     */ 

    private void Start()
    { 
        mainCamera = Camera.main;
        emitter = emitterObj.GetComponent<IEmitter2D>();
    }

    private void Update()
    {
        // If input button is registered, emit
        if(InputExt.GetButton(emitButtonName, buttonType))
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            emitter.Emit(mousePosition - (Vector2)transform.position);
        }
    }
}
