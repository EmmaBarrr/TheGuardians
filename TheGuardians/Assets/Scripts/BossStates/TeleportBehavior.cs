using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehavior : StateMachineBehaviour
{
    bool selected;
    public bool hunter;
    public float minHeight, maxHeight;
    public int team;

    public string[] state;

    public float minTime;
    public float maxTime;

    private Transform playerPos;
    private float timer;

    Vector3 target;
    public Vector3 targetDitance;

    bool nullenemy;
   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        selected = false;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        if (!hunter)
        {
            int rx = Random.Range(-2, 2);

            if (rx > 0)
            {
                target = new Vector3(playerPos.position.x + rx, animator.transform.position.y, playerPos.transform.position.z + 1.5f);
            }
            else if (rx <= 0)
            {
                target = new Vector3(playerPos.position.x + rx, animator.transform.position.y, playerPos.transform.position.z - 1.5f);
            }
        }
        else if(hunter)
        {
            target = new Vector3(playerPos.position.x, animator.transform.position.y, playerPos.transform.position.z);
        }

        animator.transform.position = target;
        if (!selected)
        {
            selected = true;
            animator.SetTrigger(state[Random.Range(0, state.Length)]);
        }
        float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
        float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
        animator.transform.position = new Vector3(animator.transform.position.x,
            animator.transform.position.y,
            Mathf.Clamp(animator.transform.position.z, minHeight, maxHeight));

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
