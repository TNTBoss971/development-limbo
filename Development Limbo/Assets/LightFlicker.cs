using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float max;
    public float min;
    public float intensity;
    private new Light light;
    private float nextFlicker;
    private bool lightOn = true;
    private float startIntensity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light>();
        startIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextFlicker <= Time.time)
        {
            // call the flicker function
            Flicker();
            // and set the time of the next flicker
            nextFlicker = Time.time + Random.Range(min, max);
        }

        if (lightOn)
        {
            light.intensity = startIntensity;
        }
        else
        {
            light.intensity = startIntensity/0.75f;
        }
    }

    private void Flicker()
    {
        float totalDelay = 0f;

        // flicker a number of times equal to the intensity
        for (int i = 0; i < intensity*2; i++)
        {
            float delay = Random.Range(totalDelay + 0.01f, totalDelay + 0.1f);
            Invoke(nameof(ToggleLight), delay);

            totalDelay += delay;
        }
    }

    private void ToggleLight()
    {
        //light.enabled = !lightOn;
        lightOn = !lightOn;
    }
}
