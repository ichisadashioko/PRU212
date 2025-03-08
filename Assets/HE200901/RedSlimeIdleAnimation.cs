using System.Collections.Generic;
using UnityEngine;

public class RedSlimeIdleAnimation : MonoBehaviour
{
    public AnimationClip idle_animation_clip;
    private AnimatorOverrideController idle_animation_aoc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Animator _animator = gameObject.GetComponent<Animator>();

        if (_animator != null)
        {
            if (idle_animation_clip == null)
            {
                idle_animation_clip = Resources.Load<AnimationClip>("red_slime_idle_animation");
            }

            if (idle_animation_clip != null)
            {
                if (idle_animation_aoc == null)
                {
                    AnimatorOverrideController aoc = new AnimatorOverrideController(_animator.runtimeAnimatorController);
                    var animation_clip_something = new List<KeyValuePair<AnimationClip, AnimationClip>>();
                    foreach (var _a in aoc.animationClips)
                    {
                        animation_clip_something.Add(new KeyValuePair<AnimationClip, AnimationClip>(_a, idle_animation_clip));
                    }

                    aoc.ApplyOverrides(animation_clip_something);
                    idle_animation_aoc = aoc;
                }

                if (idle_animation_aoc != null)
                {
                    if(_animator.runtimeAnimatorController != idle_animation_aoc)
                    {
                        _animator.runtimeAnimatorController = idle_animation_aoc;
                    }
                }
            }
        }
    }
}
