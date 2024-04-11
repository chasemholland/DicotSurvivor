using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : StateMachineBehaviour
{
    Timer walkTimer;
    float transitionTime = 2f;
    Timer multiAttack;
    BossMove boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossMove>();

        // Run multi attack timer to shoot more projectiles
        multiAttack = animator.gameObject.AddComponent<Timer>();
        multiAttack.Duration = transitionTime / 3;
        multiAttack.Run();

        // Run transition timer
        walkTimer = animator.gameObject.AddComponent<Timer>();
        walkTimer.Duration = transitionTime;
        walkTimer.Run();

        // Fire one projectile
        boss.AttackPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (multiAttack.Finished)
        {
            boss.AttackPlayer();
            multiAttack.Duration = transitionTime / 3;
            multiAttack.Run();
        }

        if (walkTimer.Finished)
        {
            Destroy(multiAttack);
            Destroy(walkTimer);
            animator.SetBool("isAttacking", false);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
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
