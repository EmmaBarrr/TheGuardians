using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Emmanuel Barrios

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    //Variables
    [SerializeField, Range (0, 24)] private float TimeOfDay;

    private void Start()
    {
        TimeOfDay = 12.4f;
        TimeOfDay %= 24;
        UpdateLighting(TimeOfDay / 24f);
    }

    private void Update()
    {
        /*if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime; // *0.004f para que un cuatro minutos reales equivalgan a 1 hora del juego
            TimeOfDay %= 24; //Clamp between 0 - 24
            UpdateLighting (TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting (TimeOfDay / 24f);
        }*/

       /* if (Input.GetKeyDown(KeyCode.Q) && Realities.mysticalWorld == false)
        {
            TimeOfDay += Time.deltaTime; // *0.004f para que un cuatro minutos reales equivalgan a 1 hora del juego
            TimeOfDay %= 24; //Clamp between 0 - 24
            UpdateLighting (TimeOfDay / 24f);
        }
        else
        {
             TimeOfDay %= 12;
            UpdateLighting (TimeOfDay / 24f);
        }*/
    }

    public void SetTimeofDay(float t, bool s)
    {
        if (s)
        {
            TimeOfDay += t;
        }
        else
        {
            TimeOfDay = t;
        }
        TimeOfDay %= 24;
        UpdateLighting(TimeOfDay / 24f);

    }

    public float getTimeOfDay()
    {
        return TimeOfDay;
    }

    private void UpdateLighting (float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate (timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate (timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate (timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler (new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    
    private void OnValidate()
    {
        if (DirectionalLight != null)
        return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

}
