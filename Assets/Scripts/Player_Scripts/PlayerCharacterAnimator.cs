using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCharacterAnimator : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement _thirdPersonPlayer = null;

    const string IdleState = "Idle";
    const string MoveState = "Walk";
    const string JumpState = "Jump";
    const string FallState = "Falling";
    const string LandState = "Landing";
    const string SprintState = "Sprinting";
    const string HitState = "Hit";
    const string DeathState = "Death";

    Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        
        _thirdPersonPlayer.Idle += OnIdle;
        _thirdPersonPlayer.StartWalking += OnStartWalking;
        _thirdPersonPlayer.Jump += OnJump;
        _thirdPersonPlayer.Fall += OnFall;
        _thirdPersonPlayer.Land += OnLand;
        _thirdPersonPlayer.StartRunning += OnStartRunning;
        _thirdPersonPlayer.GetComponent<Health>().Hit += OnHit;
        _thirdPersonPlayer.Death += OnDeath;
    }

    private void OnDisable()
    {
        _thirdPersonPlayer.Idle -= OnIdle;
        _thirdPersonPlayer.StartWalking -= OnStartWalking;
        _thirdPersonPlayer.Jump -= OnJump;
        _thirdPersonPlayer.Fall -= OnFall;
        _thirdPersonPlayer.Land -= OnLand;
        _thirdPersonPlayer.StartRunning -= OnStartRunning;
        _thirdPersonPlayer.GetComponent<Health>().Hit -= OnHit;
        _thirdPersonPlayer.Death -= OnDeath;
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

    public void OnHit()
    {
        _animator.CrossFadeInFixedTime(HitState, .1f);
    }

    public void OnDeath()
    {
        _animator.CrossFadeInFixedTime(DeathState, .2f);
    }
}
