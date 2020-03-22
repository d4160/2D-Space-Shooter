using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using UnityEngine;
using AudioBehaviour = d4160.GameFramework.AudioBehaviour;

public class EnemyIdleState : StateMachineBehaviour
{
    [SerializeField] private Vector2 _minMaxRate = new Vector2(3f, 7f);

    private float _nextFire;
    private AudioBehaviour _audio;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        _audio = animator.GetComponent<AudioBehaviour>();

        _nextFire = Time.time + _minMaxRate.Random() / 2;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + _minMaxRate.Random();
            var provider = SingleplayerModeManager.Instance.As<SingleplayerModeManager>().EnemyLaserSpawnProvider;
            provider.OverrideSpawnPosition = animator.transform.position;
            provider.Spawn();

            _audio.PlayAudioClip(1);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
