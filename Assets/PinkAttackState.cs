using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkAttackState : StateMachineBehaviour
{
    Timer attackTimer;
    float shootDelay = 1f;
    PinkEnemy pinkEnemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Start particle system
        animator.GetComponentInChildren<ParticleSystem>().Play();

        // Get reference to pink script to handle attacking
        pinkEnemy = animator.GetComponent<PinkEnemy>();

        // Run attack timer
        attackTimer = animator.gameObject.AddComponent<Timer>();
        attackTimer.Duration = shootDelay;
        attackTimer.Run();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackTimer.Finished)
        {
            // Perform attack
            bool success = pinkEnemy.Attack();

            // Stop particle system
            animator.GetComponentInChildren<ParticleSystem>().Stop();

            // Destroy pink enemy if attack successful
            if (success)
            {
                Destroy(animator.gameObject);
            }
            // Return to walk state if attack unsuccessful
            else
            {
                Destroy(attackTimer);
                animator.SetBool("isAttacking", false);
            }
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
