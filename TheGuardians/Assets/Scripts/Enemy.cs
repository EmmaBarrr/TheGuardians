using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Emmanuel Barrios Martínez

public class Enemy : MonoBehaviour
{
    private NavMeshAgent SmokeMonster;
    public float detectRadio;
    public GameObject target;
    public Animator animMonster;


    // Start is called before the first frame update
    void Start()
    {
        SmokeMonster = GetComponent<NavMeshAgent>();
        //animMonster = GetComponent<Animator>();
        //transform.position = new Vector3 (675.46f, 0f, 704.62f);
        //GameManager.mojiesCount++;

    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
    }

    /* void OnCollisionEnter(Collision col)
    {
        //Debug.Log("colisioné con ");
        if (col.gameObject.tag == "Pared")
        {
            //GameManager.mojiesCount--;
            Destroy(this.gameObject);
        } 
    } */

    public void FindTarget()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < detectRadio)
        {
            animMonster.SetBool("Run", true);
            SmokeMonster.destination = target.transform.position;
        }
        else
        {
            animMonster.SetBool("Run", false);
            SmokeMonster.destination = transform.position; //para que no se mueva
        }
    }

    public void SetTarget(bool t)
    {
        target.SetActive(t);
    }
}
