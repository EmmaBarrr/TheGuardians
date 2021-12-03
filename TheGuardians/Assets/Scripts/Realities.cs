using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realities : MonoBehaviour
{
    public static bool mysticalWorld;

    // Start is called before the first frame update
    void Start()
    {
        mysticalWorld = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && mysticalWorld == false)
        {
        mysticalWorld = true;
        }
        else
        {
            mysticalWorld = false;
        }
    }
}
