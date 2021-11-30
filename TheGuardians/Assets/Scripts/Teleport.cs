using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emmanuel Barrios

public class Teleport : MonoBehaviour
{
    public float xPosition, yPosition, zPosition;
    
    void OnCollisionEnter(Collision col)
    {
            col.gameObject.transform.position = new Vector3 (xPosition, yPosition, zPosition);
    }
}
