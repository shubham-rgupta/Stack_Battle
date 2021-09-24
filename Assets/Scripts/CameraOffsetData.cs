using UnityEngine;

public class CameraOffsetData : MonoBehaviour
{
    public bool useLocalCoordiantes;
    public Vector3 offset;
    public Vector3 lookOffset;
    public bool changeFov = false;
    public float newFov = 60f;
}