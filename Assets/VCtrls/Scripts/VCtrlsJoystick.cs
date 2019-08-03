using UnityEngine;
using System.Collections;


[AddComponentMenu("Dragonhill/Virtual Controls/Joystick")]
public class VCtrlsJoystick : MonoBehaviour
{
    public Constraints 
		constraints;

    public bool 
		appearOnPress;

    private RectTransform 
		joyStickRef,
    	foreGround;

    private Vector2 
		location;
    
	private float 
		radius;

    private Vector2 
		orgLoc;

    #if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
    private int touchID = 0;
    #endif

    void Awake()
    {
        joyStickRef = transform.Find("Joystick").GetComponent<RectTransform>();
		foreGround = joyStickRef.Find("FG").GetComponent<RectTransform>();

		orgLoc = foreGround.position;
        location = joyStickRef.position;
        radius = joyStickRef.sizeDelta.x / 2;
    }

    void Start()
    {
        if ( appearOnPress )
            ShowChildren(false);
    }

    public void OnPress(bool pressed)
    {
        if ( appearOnPress )
        {
            if (pressed)
            {
                joyStickRef.position = GetPos(true);
                ShowChildren(true);
				orgLoc = foreGround.position;
                location = joyStickRef.position;
            }

            else
            {
                ShowChildren(false);
            }
        }

        if (pressed)
			foreGround.position = GetPos(true);

        else
			foreGround.position = orgLoc;
    }

    public void OnDrag()
    {
        Vector2 point = GetPos();
        if (InRange(location, point, radius))
        {
            if (constraints == Constraints.Horizontal)
                point = new Vector3(point.x, orgLoc.y);
            if (constraints == Constraints.Vertical)
                point = new Vector3(orgLoc.x, point.y);
			foreGround.position = point;
        }
        else
        {
            Vector3 dir = point - location;
            Ray ray = new Ray(location, dir);
            point = ray.GetPoint(radius);
            if (constraints == Constraints.Horizontal)
                point = new Vector3(point.x, orgLoc.y);
            if (constraints == Constraints.Vertical)
                point = new Vector3(orgLoc.x, point.y);
			foreGround.position = point;
        }
    }

    bool InRange(Vector2 center, Vector2 point, float radius)
    {
        float sqrDist = Mathf.Pow((center.x - point.x), 2) + Mathf.Pow((center.y - point.y), 2);
        return sqrDist < Mathf.Pow(radius, 2);
    }

    void ShowChildren(bool value)
    {
        foreach (Transform child in joyStickRef)
        {
            child.gameObject.SetActive(value);
        }
    }

    public Vector3 GetVector()
    {
		return foreGround.localPosition / radius;
    }

    public float GetAxis(int index)
    {
        return GetVector()[index];
    }

    public float GetAxis(string AxisName)
    {
        if (AxisName == "Horizontal" || AxisName == "horizontal") { return GetAxis(0); }
        else if (AxisName == "Vertical" || AxisName == "vertical") { return GetAxis(1); }
        return GetAxis(0);
    }

    public Vector2 GetPos()
    {
        return GetPos(false);
    }

    public Vector2 GetPos(bool newTouch)
    {
        #if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
        if (newTouch)
            touchID = Input.touches.Length - 1;
        return Input.touches[touchID].position;
        #else
        return Input.mousePosition;
        #endif
    }
}
