using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkWalkState : StateMachineBehaviour
{
    Timer attackTimer;
    GameObject player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer = animator.gameObject.AddComponent<Timer>();
        attackTimer.Duration = Random.Range(10f, 14f);
        attackTimer.Run();

        // Get refence to player -- destroy attack timer if player is dead or if game object is mini pink slime
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || animator.name.StartsWith("Mini"))
        {
            Destroy(attackTimer);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Attack timer becomes null when player dies
        if (attackTimer != null)
        {
            if (attackTimer.Finished)
            {
                Destroy(attackTimer);
                animator.SetBool("isAttacking", true);
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
