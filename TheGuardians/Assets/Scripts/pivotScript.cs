using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rodolfo León Gasca A01653185

public class pivotScript : MonoBehaviour
{
    public Transform pivot;


    // Update is called once per frame
    void Update()
    {
        if (pivot != null)
        {
            transform.LookAt(pivot);
        }
    }
}
