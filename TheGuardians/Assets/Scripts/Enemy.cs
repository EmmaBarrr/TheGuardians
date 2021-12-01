using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Rodolfo León Gasca A01653185

public class Enemy : MonoBehaviour
{
    PlayerMovementCC pl;
    public NavMeshAgent agent;
    public float Distance;
    public GameObject target;

    public GameObject Normal, Magic;
    // Start is called before the first frame update
    void Start()
    {
        pl = FindObjectOfType<PlayerMovementCC>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDimension(pl.isMagic);
    }

    public void SetDimension(bool b)
    {
        Normal.SetActive(!b);
        Magic.SetActive(b);
    }

    public void SetTarget(bool t)
    {
        target.SetActive(t);
    }
}
