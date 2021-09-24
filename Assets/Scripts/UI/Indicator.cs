using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    public RectTransform indicatorRt;
    public Camera mainCam;
    Transform playerTransform;
    void Start()
    {
        mainCam = Camera.main;
        playerTransform = PlayerMove.obj.transform;
    }
    void Update()
    {
        //get world to screen point
        //Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 imagePos = ClampPos(screenPos);
        //imagePos.z = 0;
        // indicatorRt.transform.position = imagePos;
        //indicatorRt.gameObject.SetActive(OutOfBounds(screenPos));
        //indicatorRt.transform.up = (screenPos - imagePos).normalized;


        //Debug.DrawRay(Camera.main.transform.position,GetDirectionVector(transform.position)*10f,Color.red);

        Vector3 worldDir = (transform.position - playerTransform.position).normalized;
        indicatorRt.gameObject.SetActive(OutOfBounds(Camera.main.WorldToScreenPoint(transform.position)));
        Vector3 screenDir = GetDirectionVector(worldDir);
        Vector3 screenPos = GetScreenPos(screenDir);

        indicatorRt.transform.position = screenPos;
        indicatorRt.transform.up = screenDir;
    }

    Vector3 ClampPos(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x,0, Screen.width);
        pos.y = Mathf.Clamp(pos.y,0, Screen.height);
        return pos;
    }

    bool OutOfBounds(Vector3 pos)
    {
        return !(pos.x > 0 && pos.x < Screen.width && pos.y > 0 && pos.y < Screen.height);
    }

    Vector3 GetDirectionVector(Vector3 worldDir)
    {
        return Vector3.ProjectOnPlane(worldDir, Camera.main.transform.forward);
    }

    Vector3 GetScreenPos(Vector3 screenDir)
    {
        //find angle
        Vector3 screenpos = new Vector3(Screen.width/2,Screen.height/2,0);
        
        //marching towards the edge of the screen, until edge point is found

        while(!OutOfBounds(screenpos))
        {
            screenpos += screenDir;
        }

        return ClampPos(screenpos);
    }
}
