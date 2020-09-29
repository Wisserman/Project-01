using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class ThirdPersonMovement : MonoBehaviour, IControlable
{
    public Animator animator;

    public event Action Idle = delegate { };
    public event Action StartWalking = delegate { };
    public event Action StartRunning = delegate { };
    public event Action Jump = delegate { };
    public event Action Fall = delegate { };
    public event Action Land = delegate { };
    public event Action Death = delegate { };

    [SerializeField] Canvas canvas;
    [SerializeField] GameObject baseUI;
    GameObject playerUI;

    UnityEvent LandEvent = new UnityEvent();

    public CharacterController controller;
    public Transform camMain;

    public float speed;
    public float walkSpeed;
    public float runSpeed;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float gravForce;
    public float jSpeed;
    float vSpeed = -0.1f;

    string characterState = "Idle";

    AudioPlayer _audioPlayer;
    ParticleScript _particleScript;

    Vector3 direction;

    public Transform[] enemies;

    private void Awake()
    {
        Control(baseUI);
        _audioPlayer = GetComponentInChildren<AudioPlayer>();
        _particleScript = GetComponent<ParticleScript>();
    }

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
        if (speed > 0)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            direction = new Vector3(horizontal, 0, vertical).normalized;
        }

        Vector3 moveDir;

        if (direction.magnitude >= 0.2f && characterState != "Landing")
        {
                if (Input.GetKey(KeyCode.LeftShift))
                    CheckIfStartRunning();
                else
                    CheckIfStartWalking();
            

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camMain.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle + 45f, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0f, vSpeed, 1);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            if(characterState != "Landing")
                CheckIfStopMoving();

            moveDir = new Vector3(0f, vSpeed, 0);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
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

            if (characterState == "Jumping")
            {
                Fall?.Invoke();
                characterState = "Falling";
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
            Control(baseUI);
        if (Input.GetKeyDown(KeyCode.X))
            Release();
    }

    

    private void CheckIfStartWalking()
    {
        if (characterState != "Walking" && characterState != "Falling" && controller.isGrounded == true)
        {
            speed = walkSpeed;
            StartWalking?.Invoke();
            characterState = "Walking";
            _audioPlayer.PlayClip(characterState);
        }
    }

    private void CheckIfStartRunning()
    {
        if (characterState != "Sprinting" && characterState != "Falling" && controller.isGrounded == true)
        {
            speed = runSpeed;
            StartRunning?.Invoke();
            characterState = "Sprinting";
            _audioPlayer.PlayClip(characterState);
            Debug.Log(characterState);
        }

    }

    private void CheckIfStopMoving()
    {
        if (characterState != "Idle" && characterState != "Falling" && controller.isGrounded == true)
        {
            Idle?.Invoke();
            characterState = "Idle";
            _audioPlayer.PlayClip(characterState);
            speed = walkSpeed;
        }
    }

    private IEnumerator LandedAnimation()
    {
            Land?.Invoke();
            yield return new WaitForSeconds(0.45f);
            characterState = "Landed";
    }

    private IEnumerator JumpTimer()
    {
        vSpeed = jSpeed;
        Jump?.Invoke();
        _audioPlayer.PlayClip("Jumping");
        yield return new WaitForSeconds(0.45f);
        characterState = "Jumping";

    }

    void Landed()
    {
        if (controller.isGrounded && characterState == "Falling")
        {
            StartCoroutine("LandedAnimation");
            characterState = "Landing";
            _audioPlayer.PlayClip(characterState);
        }
    }

    public Transform GetClosestEnemy()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    public void Control(GameObject newUI)
    {
        Release();

        playerUI = Instantiate(newUI);

        // set as child of Canvas
        playerUI.transform.SetParent(canvas.transform);
    }

    public void Release()
    {
        Death?.Invoke();
        speed = 0;
        foreach (Transform child in canvas.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
