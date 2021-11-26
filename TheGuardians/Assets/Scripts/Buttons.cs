using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void GoToScene(int index){
        SceneManager.LoadScene(index);
    }

    public void GoToScene(string sceneName)
    {
        //Aqui vamos a loadear la escena
        SceneManager.LoadScene(sceneName);
    }
}
