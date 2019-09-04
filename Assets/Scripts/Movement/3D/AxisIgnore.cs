[System.Serializable]
public struct AxisIgnore
{
    public bool xIgnore;
    public bool yIgnore;
    public bool zIgnore;

    public AxisIgnore(bool x, bool y, bool z)
    {
        xIgnore = x;
        yIgnore = y;
        zIgnore = z;
    }
}
