using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyanAttackState : StateMachineBehaviour
{
    int projectilesShot;
    Timer attackTimer;
    float shootDelay = 0.5f;
    Enemy enemy;
    bool hasShot;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Start particle system
        animator.GetComponentInChildren<ParticleSystem>().Play();

        // Get reference to boss script to handle attacking
        enemy = animator.GetComponent<Enemy>();

        // Run multi attack timer to shoot more projectiles
        attackTimer = animator.gameObject.AddComponent<Timer>();
        attackTimer.Duration = shootDelay;
        attackTimer.Run();

        // Has not shot yet
        hasShot = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackTimer.Finished && !hasShot)
        {
            enemy.AttackPlayer();
            hasShot = true;
            attackTimer.Duration = shootDelay;
            attackTimer.Run();
        }
        else if (attackTimer.Finished && hasShot) 
        {
            // Stop particle system
            animator.GetComponentInChildren<ParticleSystem>().Stop();
            Destroy(attackTimer);
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
