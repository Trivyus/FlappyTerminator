using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    public event Action DeathAnimationComleted;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateJumping(bool isJumping)
    {
        _animator.SetBool(AnimatorData.Params.JumpingHash, isJumping);
    }

    public void UpdateFalling(bool isFalling)
    {
        _animator.SetBool(AnimatorData.Params.FallingHash, isFalling);
    }

    public void TriggerAtack()
    {
        _animator.SetTrigger(AnimatorData.Params.AttackingHash);
    }

    public void TriggerDeath()
    {
        _animator.SetTrigger(AnimatorData.Params.DyingHash);

        float animationLength = GetCurrentAnimationLength();
        Invoke(nameof(HandleDeathAnimationCompletion), animationLength);
    }

    private float GetCurrentAnimationLength()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length;
    }

    private void HandleDeathAnimationCompletion()
    {
        DeathAnimationComleted?.Invoke();
    }
}
