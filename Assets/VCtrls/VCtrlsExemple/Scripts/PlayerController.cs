using UnityEngine;
using System.Collections;


[RequireComponent(typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
   

	private CharacterController 
		controller;
	
	[SerializeField]public float 
        sensitivityX = 3f,
        sensitivityY = 3f,
		speed = 6.0F;
	
	private Vector3 rotationXYZ = new Vector3( 0.0f,0.0f,0.0f);

	[SerializeField]private Vector3 
		moveDirection = Vector3.zero,
		dirVec;

	private Vector2 
		myDir;

	public float
		minEngineVol = 0.3f,
		maxEngineVol = 2.0f,
		engineVol,
		rangeVol;

    void Awake()
    {
		controller = GetComponent<CharacterController>();
    }

	void Start(){

	}

    void FixedUpdate()
    {
		MovePlayer();
       	Rotation();
    }

    void Rotation()
    {
        rotationXYZ.x = transform.localEulerAngles.y + VCtrlsManager.Stick["Stick01"].GetAxis(0) * sensitivityX;
        transform.localEulerAngles = new Vector3(-rotationXYZ.y, rotationXYZ.x, rotationXYZ.z);
    }

	void MovePlayer()
	{
        /*  Not used ATM
		bool moveForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
		bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
		bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
		bool moveBack = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

		//euler.y += Input.GetAxis("Mouse X") * rotateInfluence * 3.25f;
        */

		myDir = VCtrlsManager.Stick["Stick02"].GetVector();
		dirVec = new Vector3(0, 0, myDir.y);

		dirVec = transform.TransformDirection(dirVec);
		dirVec *= speed;

		controller.Move(dirVec * Time.deltaTime);			//move the players controller

	}

}
