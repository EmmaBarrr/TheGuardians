using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCempa : MonoBehaviour
{
    private bool MagicVision;
    public GameObject Canva;
    public GameObject way;
    public static float time;
    public float seconds;
    // Start is called before the first frame update
    void Start()
    {
        MagicVision = false;
        time = seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (MagicVision == false)
        {
            Canva.gameObject.SetActive(false);
            way.gameObject.SetActive(false);
        }
        else
        {
            Canva.gameObject.SetActive(true);
            way.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(VisionMagica());
        }
    }

    IEnumerator VisionMagica()
    {
        MagicVision = true;
            yield return new WaitForSeconds(time);
            MagicVision = false;
    }
}
