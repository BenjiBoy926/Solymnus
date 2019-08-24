using UnityEngine;

[System.Serializable]
public class Anchor
{
    public Vector3 origin;
    public Vector3 direction;

    public Anchor(Vector3 org, Vector3 dir)
    {
        origin = org;
        direction = dir;
    }
}
