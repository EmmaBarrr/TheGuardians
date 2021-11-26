using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    public static Soundtrack music;

    private void Start()
    {
        //Para que no se duplique el sonido & si se duplica que se destruya
        if (music == null) {
        music = this;
        } else {
            Destroy(this.gameObject);
        }

    //Para que no se destruya mientras se está ejecutando
    DontDestroyOnLoad(gameObject);
    }
}
