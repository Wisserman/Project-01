using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotationController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CinemachineTargetGroup targetGroup;
    CinemachineFreeLook lockOnLookAt;
    public float lockOnDist;

    bool cameraMove = false;
    bool lockedOn = false;
    ThirdPersonMovement playerMoveScript;

    private void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
        playerMoveScript = player.GetComponent<ThirdPersonMovement>();
        lockOnLookAt = GetComponent<CinemachineFreeLook>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            cameraMove = true;
            CinemachineCore.GetInputAxis = GetAxisCustom;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            cameraMove = false;
            CinemachineCore.GetInputAxis = GetAxisCustom;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            lockedOn = !lockedOn;
            LockOn();
        }
        else if (lockedOn && Vector3.Distance(player.transform.position,
            playerMoveScript.GetClosestEnemy().position) > lockOnDist)
            LockOn();

    }


    private float GetAxisCustom(string axisName)
    {
        if (cameraMove)
            return UnityEngine.Input.GetAxis(axisName);
        else
            return UnityEngine.Input.GetAxis(axisName)*0;
    }

    private void LockOn()
    {
        if (lockedOn && Vector3.Distance(player.transform.position, playerMoveScript.GetClosestEnemy().position) <= lockOnDist)
            targetGroup.AddMember(playerMoveScript.GetClosestEnemy().transform, 1f, 10f);
        else
            targetGroup.RemoveMember(playerMoveScript.GetClosestEnemy());
    }
}
