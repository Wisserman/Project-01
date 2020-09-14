using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotationController : MonoBehaviour
{
    bool cameraLook = false;

    private void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            cameraLook = true;
            CinemachineCore.GetInputAxis = GetAxisCustom;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            cameraLook = false;
            CinemachineCore.GetInputAxis = GetAxisCustom;
        }
    }


    private float GetAxisCustom(string axisName)
    {
        if (cameraLook)
            return UnityEngine.Input.GetAxis(axisName);
        else
            return UnityEngine.Input.GetAxis(axisName)*0;
    }
}
