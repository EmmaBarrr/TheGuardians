using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Emmanuel Barrios

public class TimerBar : MonoBehaviour
{
    public Slider timerSlider;
    public float timeGame;
    //private bool stopTimer;

    // Start is called before the first frame update
    void Start()
    {
        //cuando choque con la calaverita 
        //stopTimer = false;
        timerSlider.maxValue = ActivateCempa.time;
        timeGame = ActivateCempa.time;
        //timerSlider.value = timeGame;
        //StartCoroutine (countDown());

    }

    // Update is called once per frame
    void Update()
    {
        timerSlider.value = timeGame;
        //timeGame -= 1f;
       if (timeGame > 0)
        {
            StartCoroutine(countDown());
        } 

        if (timeGame == 0)
        {
            timeGame = ActivateCempa.time;
        }


    }

    IEnumerator countDown()
    {
        
            timeGame -= 0.04f;
            yield return new WaitForSeconds(1f);
            //Debug.Log (timeGame);
    }
}
