using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed;
    public float smoothSpeedRot;
    private Vector3 vel = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        //Vector3 tempPos = Vector3.SmoothDamp(transform.position, desiredPosition, ref vel, smoothSpeed);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, smoothSpeedRot * Time.deltaTime);
        Vector3 tempRot = transform.eulerAngles;
        tempRot.x = 0f;
        tempRot.z = 0f;
        transform.eulerAngles = tempRot;
        
    }
}
