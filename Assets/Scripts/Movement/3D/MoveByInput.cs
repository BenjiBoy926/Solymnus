using UnityEngine;

/*
 * CLASS MoveByInput
 * -----------------
 * Translates 2-D button inputs into 3-D movement in a plane
 * -----------------
 */ 

public class MoveByInput : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the script that controls the movement of a kinemetic mover")]
    private KinematicMoverController controller;
    [SerializeField]
    [Tooltip("Vector orthogonal to the plane that the mover moves around in")]
    private Vector3 planeVector;
    [SerializeField]
    [Tooltip("Transform component of the camera used to view this moving object")]
    private Transform cameraTransform;
    [SerializeField]
    private string horizontalButtonName;    // Name of the button in the input manager used to move sideways
    [SerializeField]
    private string verticalButtonName;  // Name of the button in the input manager used to move vertically
    [SerializeField]
    [Tooltip("If true, outputs are -1, 0, 1.  Otherwise, " +
        "outputs are interpolated decimals between -1 and 1")]
    private bool raw;

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = new Vector2();
        Vector3 velocity;   // Exact velocity set on the rigidbody

        // The forward direction of the agent's coordinates, ignoring camera angle
        Vector3 absoluteForward = Vector2.up.MapToPlane(planeVector);
        // The forward direction of the agent's coordinates relative to the camera angle
        Vector3 cameraRelativeForward = Vector3.ProjectOnPlane(cameraTransform.forward, planeVector);

        if (raw)
        {
            inputVector.x = Input.GetAxisRaw(horizontalButtonName);
            inputVector.y = Input.GetAxisRaw(verticalButtonName);
        }
        else
        {
            inputVector.x = Input.GetAxis(horizontalButtonName);
            inputVector.y = Input.GetAxis(verticalButtonName);
        }

        // Map the 2D vector onto the given plane
        velocity = inputVector.MapToPlane(planeVector);

        // Apply the same transformation to the velocity that
        // changes the absolute forward of the agent to the forward relative to camera angle
        velocity = velocity.Transform(absoluteForward, cameraRelativeForward);
        
        controller.Move(velocity);
    }
}
