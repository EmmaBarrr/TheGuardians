using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoMortal : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //GameManager.mojiesCount--;
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        } 
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
