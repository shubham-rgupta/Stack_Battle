using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    //public Transform player;
    //public Vector3 offset;
    //public float smoothSpeed;
    //private Vector3 vel = Vector3.zero;
    //public float dis;


    //public CinemachineVirtualCamera followCam;
    //public float distance = 1f;
    //public Transform target;
    ////private void Update()
    ////{
    ////    if (Input.GetKeyDown(KeyCode.KeypadPlus))
    ////        IncreseCamDistance();
    ////}
    //public void IncreseCamDistance()
    //{
    //    distance += .0005f;
    //    var transposer = followCam.GetCinemachineComponent<CinemachineTransposer>();
    //    transposer.m_FollowOffset = transposer.m_FollowOffset * distance;
    //}
    //public void DecreseCamDistance()
    //{
    //    distance -= .0005f;
    //    var transposer = followCam.GetCinemachineComponent<CinemachineTransposer>();
    //    transposer.m_FollowOffset = transposer.m_FollowOffset * distance;
    //}
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed;
    private Vector3 vel = Vector3.zero;
    public float dis;
    private void LateUpdate()
    {
        if (player != null)
        {
            dis = PlayerMove.obj.listLength * 0.5f;
            Vector3 desiredPosition = player.position + (offset - new Vector3(0, 0, dis));
            //transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref vel, smoothSpeed);
            Vector3 pos = Damp(transform.position, desiredPosition, smoothSpeed);
            transform.position = pos;
        }
        
        //Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //transform.position = smoothPosition;
        //transform.LookAt(player);
    }
    public static Vector3 Damp(Vector3 a, Vector3 b, float smoothing)
    {
        return Vector3.Lerp(a, b, 1 - Mathf.Exp(-smoothing * Time.deltaTime));
    }

}
