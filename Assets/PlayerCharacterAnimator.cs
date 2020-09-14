using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCharacterAnimator : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement _thirdPersonMovement = null;

    const string IdleState = "Idle";
    const string MoveState = "Walk";
    const string JumpState = "Jump";
    const string FallState = "Falling";
    const string LandState = "Landing";
    const string SprintState = "Sprinting";

    Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _thirdPersonMovement.Idle += OnIdle;
        _thirdPersonMovement.StartWalking += OnStartWalking;
        _thirdPersonMovement.Jump += OnJump;
        _thirdPersonMovement.Fall += OnFall;
        _thirdPersonMovement.Land += OnLand;
        _thirdPersonMovement.StartRunning += OnStartRunning;
    }

    private void OnDisable()
    {
        _thirdPersonMovement.Idle -= OnIdle;
        _thirdPersonMovement.StartWalking -= OnStartWalking;
        _thirdPersonMovement.Jump -= OnJump;
        _thirdPersonMovement.Fall -= OnFall;
        _thirdPersonMovement.Land -= OnLand;
        _thirdPersonMovement.StartRunning -= OnStartRunning;
    }

    private void OnStartWalking()
    {
        _animator.CrossFadeInFixedTime(MoveState, .2f);
    }

    public void OnIdle()
    {
        _animator.CrossFadeInFixedTime(IdleState, .2f);
    }

    public void OnJump()
    {
        _animator.Play(JumpState);
    }

    public void OnFall()
    {
        _animator.CrossFadeInFixedTime(FallState, .2f);
    }

    public void OnLand()
    {
        _animator.CrossFadeInFixedTime(LandState, .2f);
    }

    public void OnStartRunning()
    {
        _animator.CrossFadeInFixedTime(SprintState, .2f);
    }
}
