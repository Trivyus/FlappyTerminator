using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorData
{
    public static class Params
    {
        public static readonly int JumpingHash = Animator.StringToHash("IsJumping");
        public static readonly int FallingHash = Animator.StringToHash("IsFalling");
        public static readonly int AttackingHash = Animator.StringToHash("Attack");
        public static readonly int DyingHash = Animator.StringToHash("Die");
    }
}