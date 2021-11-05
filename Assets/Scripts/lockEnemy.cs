using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class lockEnemy : MonoBehaviour
{
    public GameObject cam1, cam2;
    public bool lo;
    public float distance;
    public CinemachineTargetGroup tg;
    public GameObject closestEnemy = null, closestTarget = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    public bool Islocked()
    {

        closestEnemy = null;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        float distanceToClosestEnemy = Mathf.Infinity;

        tg.m_Targets = null;
        tg.AddMember(this.transform, 10, 10);

        foreach (GameObject g in gos)
        {
            float distanceToEnemy = (g.transform.position - this.transform.position).sqrMagnitude;

            if (distanceToEnemy < distanceToClosestEnemy)
            {
                if (distanceToEnemy < distance)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = g;
                }
            }
        }
        if (closestEnemy != null)
        {

            if (Vector3.Distance(transform.position, closestEnemy.transform.position) <= distance)
            {
                tg.AddMember(closestEnemy.transform, 30, 10);
            }
        }

        return closestEnemy != null;
    }

    public void TargetLock()
    {
        if (!lo)
        {
            closestTarget = null;
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Enemy");

            float distanceToClosestEnemy = Mathf.Infinity;

            tg.m_Targets = null;
            tg.AddMember(this.transform, 10, 10);

            foreach (GameObject g in gos)
            {
                g.GetComponent<Enemy>().SetTarget(false);
                float distanceToEnemy = (g.transform.position - this.transform.position).sqrMagnitude;

                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    if (distanceToEnemy < distance)
                    {
                        distanceToClosestEnemy = distanceToEnemy;
                        closestTarget = g;
                    }
                }
            }
            if (closestTarget != null)
            {

                if (Vector3.Distance(transform.position, closestTarget.transform.position) <= distance)
                {
                    closestTarget.GetComponent<Enemy>().SetTarget(true);
                }
            }
        }
    }

    public void Update()
    {
        //cam1.SetActive(!lo);
        //cam2.SetActive(lo);

        TargetLock();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            lo = Islocked();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            lo = false;
            tg.m_Targets = null;
            tg.AddMember(this.transform, 10, 10);
        }
    }

    public GameObject ReturnEnemy()
    {
        return closestEnemy;
    }
}
