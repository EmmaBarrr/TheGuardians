using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehavior : StateMachineBehaviour
{
    bool selected;
    public float minHeight, maxHeight;
    public int team;

    public string[] state;

    public float minTime;
    public float maxTime;

    private Transform playerPos;
    private float timer;
    public float speed;

    public bool charge;
    public bool roll;

    public bool retreat;
    public float range;

    bool nullenemy;

    Vector3 target;
    public Vector3 targetDitance;

    float hForce;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        selected = false;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(playerPos.position.x, animator.transform.position.y, playerPos.transform.position.z);
        
        timer = Random.Range(minTime, maxTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;

        if (!roll)
        {
            targetDitance = playerPos.position - animator.transform.position;
            hForce = targetDitance.x / Mathf.Abs(targetDitance.x);

            float zForce = Random.Range(-1, 2);

            if (Mathf.Abs(targetDitance.x) < 1.3f)
            {
                zForce = targetDitance.z / Mathf.Abs(targetDitance.z);
            }
            if (Mathf.Abs(targetDitance.x) < range)
            {
                hForce = 0;
            }
            if (Mathf.Abs(targetDitance.z) < 0.1f)
            {
                zForce = 0;
            }


            target = new Vector3(animator.transform.position.x + hForce, animator.transform.position.y, animator.transform.position.z + zForce);
        }

        animator.transform.position = Vector3.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);

        if (!nullenemy)
        {
            if (retreat)
            {
                if (Mathf.Abs(targetDitance.x) > range && Mathf.Abs(targetDitance.z) < 0.2f)
                {
                    if (!selected)
                    {
                        selected = true;
                        animator.SetTrigger("Attack");
                    }

                }
            }
            if (!retreat)
            {
                if (Mathf.Abs(targetDitance.x) < range && Mathf.Abs(targetDitance.z) < 0.2f)
                {
                        if (!selected)
                        {
                            selected = true;
                            animator.SetTrigger("Attack");
                        }

                }
            }
        }

        if (timer <= 0)
        {
            if (!selected)
            {
                selected = true;
                animator.SetTrigger(state[Random.Range(0, state.Length)]);
            }
        }
    }


}