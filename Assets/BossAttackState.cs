using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : StateMachineBehaviour
{
    int projectilesShot;
    Timer multiAttack;
    float shootDelay = 0.05f;
    BossEnemy boss;
    int numProjectiles = 12;
    int currentProjectile;
    float radius = 0.5f;
    float angleStep;
    int clockWise = -1;
    Vector3 startPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Start particle system
        animator.GetComponentInChildren<ParticleSystem>().Play();

        // Get reference to boss script to handle attacking
        boss = animator.GetComponent<BossEnemy>();

        // Run multi attack timer to shoot more projectiles
        multiAttack = animator.gameObject.AddComponent<Timer>();
        multiAttack.Duration = shootDelay;
        multiAttack.Run();

        // Get angle between projectiles and rotation of projectile spawn via clockWise
        angleStep = 360f / numProjectiles * clockWise;
        projectilesShot = 0;
        currentProjectile = 1;

        // Get start position of attack circle
        int start = Random.Range(1, 5);
        switch (start)
        {
            case 1:
                startPos = Vector3.right;
                break;
            case 2:
                startPos = Vector3.left;
                break;
            case 3:
                startPos = Vector3.up;
                break;
            case 4:
                startPos = Vector3.down;
                break;
            default:
                startPos = Vector3.right;
                break;
        }

        // Fire one projectile
        boss.AttackPlayer(currentProjectile, angleStep, radius, startPos);
        currentProjectile++;
        projectilesShot++;
        clockWise *= -1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (multiAttack.Finished)
        {
            boss.AttackPlayer(currentProjectile, angleStep, radius, startPos);
            currentProjectile++;
            projectilesShot++;
            multiAttack.Duration = shootDelay;
            multiAttack.Run();
        }

        // Check if all projectiles have been shot
        if (projectilesShot == numProjectiles)
        {
            // Stop particle system
            animator.GetComponentInChildren<ParticleSystem>().Stop();
            Destroy(multiAttack);
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
