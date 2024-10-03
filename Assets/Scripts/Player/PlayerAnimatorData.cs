using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int IsAtacked = Animator.StringToHash(nameof(IsAtacked));
        public static readonly int IsHeal = Animator.StringToHash(nameof(IsHeal));
    }
}

