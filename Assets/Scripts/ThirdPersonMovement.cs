using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    public Animator animator;

    public event Action Idle = delegate { };
    public event Action StartWalking = delegate { };
    public event Action StartRunning = delegate { };
    public event Action Jump = delegate { };
    public event Action Fall = delegate { };
    public event Action Land = delegate { };

    UnityEvent LandEvent = new UnityEvent();

    public CharacterController controller;
    public Transform camMain;

    float speed;
    public float walkSpeed;
    public float runSpeed;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float gravForce;
    public float jSpeed;
    float vSpeed = -0.1f;

    string _animationState = "Idle";

    Vector3 direction;

    private void Start()
    {
        Idle?.Invoke();
        speed = walkSpeed;

        LandEvent.AddListener(Landed);
    }

    // Update is called once per frame
    void Update()
    {
        // Get and Set x/z direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 moveDir;

        if (direction.magnitude >= 0.2f)
        {
            if (_animationState != "Landing")
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    CheckIfStartRunning();
                else
                    CheckIfStartWalking();
            }

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camMain.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle + 45f, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0f, vSpeed, 1);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            if(_animationState != "Landing")
                CheckIfStopMoving();

            moveDir = new Vector3(0f, vSpeed, 0);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown("space"))
            {
                StartCoroutine("JumpTimer");
            }
            else
            {
                LandEvent.Invoke();
                vSpeed = -0.1f;
            }
        }
        else if (transform.position.y > 0.03f)
        {
            vSpeed -= gravForce * Time.deltaTime;

            if (_animationState == "Jumping")
            {
                Fall?.Invoke();
                _animationState = "Falling";
            }
        }
    }



    private void CheckIfStartWalking()
    {
        if (_animationState != "Walking" && _animationState != "Falling" && controller.isGrounded == true)
        {
            speed = walkSpeed;
            StartWalking?.Invoke();
            _animationState = "Walking";
        }
    }

    private void CheckIfStartRunning()
    {
        if (_animationState != "Sprinting" && _animationState != "Falling" && controller.isGrounded == true)
        {
            speed = runSpeed;
            StartRunning?.Invoke();
            _animationState = "Sprinting";
        }

    }

    private void CheckIfStopMoving()
    {
        if (_animationState != "Idle" && _animationState != "Falling" && controller.isGrounded == true)
        {
            Idle?.Invoke();
            _animationState = "Idle";
        }
    }

    private IEnumerator LandedAnimation()
    {
            Land?.Invoke();
            yield return new WaitForSeconds(0.45f);
            _animationState = "Landed";
    }

    private IEnumerator JumpTimer()
    {
        vSpeed = jSpeed;
        Jump?.Invoke();
        yield return new WaitForSeconds(0.45f);
        _animationState = "Jumping";
    }

    void Landed()
    {
        if (controller.isGrounded && _animationState == "Falling")
            StartCoroutine("LandedAnimation");
    }
}
