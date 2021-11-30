using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Emmanuel Barrios

public class Protect : MonoBehaviour
{
    public GameObject SphereProtection;
    public GameObject Origin;
    public GameObject lightning;
    Animator animator;

   private void Awake()
   {
       animator = GetComponent<Animator>();
   }

    // Update is called once per frame
    void Update()
    {
        CreateProtect();
        LanzarHechizo();
    }

    public void CreateProtect()
    {
        
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
        //Necesitamos saber qué instanciar
        //Instanciar en posición origen
        GameObject SphereClon = Instantiate (SphereProtection, Origin.transform.position, Origin.transform.rotation);
        }

    }

    public void LanzarHechizo()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
        //Necesitamos saber qué instanciar
        //Instanciar en posición origen
        GameObject rayitoLanzado = Instantiate(lightning, Origin.transform.position, Origin.transform.rotation);
        //agregarle al rigidbody de nuestro rayito una fuerza
        //rayitoLanzado.GetComponent<Transform>().rotation.x = 90f;
        rayitoLanzado.GetComponent<Rigidbody>().velocity=Origin.transform.forward *100; //AddForce  //forward  //up
        }
    }
}
