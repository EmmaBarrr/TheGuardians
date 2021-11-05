using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Rodolfo León Gasca A01653185

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public float Distance;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(bool t)
    {
        target.SetActive(t);
    }
}
