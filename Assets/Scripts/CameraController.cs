using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    internal Camera mainCam;

    [SerializeField]GameObject target;
    [SerializeField]Vector3 offset;
    [SerializeField]bool localCoordinatesOffset = false;

    [SerializeField]float lookAtSpeed = 10;
    [SerializeField]float followSpeed = 10;

    Stack<GameObject> tempTargets;
    GameObject originalTarget;
    public float originalFov;

    //[SerializeField]CameraShake shake;

    void Awake(){
        Instance = this;
        mainCam = GetComponentInChildren<Camera>();
        originalFov = mainCam.fieldOfView;
        tempTargets = new Stack<GameObject>();
    }

    void LateUpdate()
    {
        if(target==null)return;
        Vector3 targetPosition = target.transform.position;
        CameraOffsetData offsetData = target.GetComponent<CameraOffsetData>();
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        if(offsetData!=null){
            if(offsetData.useLocalCoordiantes){
                targetPosition += target.transform.right*offsetData.offset.x + target.transform.up*offsetData.offset.y + target.transform.forward*offsetData.offset.z;
            }else{
                targetPosition += offsetData.offset;
                // targetRotation = Quaternion.LookRotation(target.transform.position - transform.position + offsetData.lookOffset);
                targetRotation = Quaternion.LookRotation(targetPosition + offsetData.lookOffset);
            }
            
        }else{
            if(localCoordinatesOffset){
                targetPosition += target.transform.right*offset.x + target.transform.up*offset.y + target.transform.forward*offset.z;
            }else{
                targetPosition += offset;
            }
        }


        transform.position = Vector3.Lerp(transform.position,targetPosition,followSpeed*Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Time.deltaTime*lookAtSpeed);
    }

    public void SetFov(float targetFov){
        float currentFov = mainCam.fieldOfView;
        LeanTween.value(gameObject,0,1f,0.5f).setOnUpdate((float val)=>{
            mainCam.fieldOfView = Mathf.Lerp(currentFov,targetFov,val);
        }).setEaseInOutSine();
    }

    public void Focus(float focusMultiplier = 2){
        offset = offset * (1/focusMultiplier);
    }

    //public void Shake(){
    //    shake.shakeDuration+=0.2f;
    //}

    public void SetTarget(GameObject newTarget){
        target = newTarget;
        CameraOffsetData offsetData = target.GetComponent<CameraOffsetData>();
        if(offsetData!=null){
            if(offsetData.changeFov){
                SetFov(offsetData.newFov);
            }else if(mainCam.fieldOfView!=originalFov){
                SetFov(originalFov);
            }
        }
    }

    public void PushTempTarget(GameObject newTempTarget){
        if(tempTargets.Count==0){
            originalTarget = target;
        }

        tempTargets.Push(newTempTarget);
        target = newTempTarget;
    }

    public void PopTempTarget(){
        tempTargets.Pop();
        if(tempTargets.Count>0){
            target = tempTargets.Peek();
        }else{
            target = originalTarget;
        }
    }
}
