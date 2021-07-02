using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAlignView : MonoBehaviour
{
    bool isTracking = false;
    public Controller control;
    public GameObject aimMidPoint, aimEndPoint, shot, aimObject;

    private void Start()
    {
        ShowAimPoints(false);
    }

    private void Update()
    {
        if (isTracking)
        {
            
            aimEndPoint.transform.SetPositionAndRotation(
                aimObject.transform.position,
                Quaternion.LookRotation(shot.transform.position - aimObject.transform.position)
                );

            Vector3 midPoint = Vector3.Lerp(
                shot.transform.position,
                aimObject.transform.position,
                0.5f
                );

            aimMidPoint.transform.SetPositionAndRotation(
                midPoint,
                Quaternion.LookRotation(shot.transform.position - midPoint)
                );

            aimMidPoint.transform.localScale = new Vector3(1, 1, (shot.transform.position - midPoint).magnitude * 1f);
        }
    }

    public void StartTracking()
    {
        shot = control.GetCurShot();
        isTracking = true;
        ShowAimPoints(true);
    }

    public void StopTracking()
    {
        isTracking = false;
        ShowAimPoints(false);
    }

    void ShowAimPoints(bool show)
    {
        aimEndPoint.SetActive(show);
        aimMidPoint.SetActive(show);
    }

    
}
