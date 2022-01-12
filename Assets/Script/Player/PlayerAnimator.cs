using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : Sing<PlayerAnimator>
{
    public Animator Player => playerAnimator;
    [SerializeField] private Animator playerAnimator;
    private AnimatorStateInfo state;
    public void PlayerSet(string AnimatorClip, bool isPlay)
    {
        playerAnimator.SetBool(AnimatorClip, isPlay);
    }
    public void PlayerPlay(string AnimatorClip)
    {
        playerAnimator.Play(AnimatorClip);
    }

    public bool IsPlaying(string name)
    {
        state = playerAnimator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName(name))
        {
            return true;
        }

        return false;
    }
    //是否播放完成
    public bool IsPlayed(string name)
    {
        state = playerAnimator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName(name) && state.normalizedTime > 1f)
        {
            return true;
        }

        return false;
    }
}

