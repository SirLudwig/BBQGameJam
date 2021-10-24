using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinnerUI : MonoBehaviour
{
    public GifDisplayer gif;

    public float defaultFrameRate;

    public Image image;

    public Color coolColor;
    public Color hotColor;

    bool isCurrentlyPlaying = false;

    private Coroutine spinnerCoroutine;

    public void UpdateColor(float value)
    {
        image.color = Color.Lerp(coolColor, hotColor, value);
    }

    public void StartPlay()
    {
        if(!isCurrentlyPlaying)
        {
            gif.framesPerSecond = 10;
            spinnerCoroutine = StartCoroutine(gif.PlayGif());
            Debug.Log("Starting");
            isCurrentlyPlaying = true;
        }
    }

    public void StopPlay()
    {
        if(isCurrentlyPlaying)
        {
            if(spinnerCoroutine != null)
            {
                StopCoroutine(spinnerCoroutine);
                Debug.Log("Stopping");
            }
            
            gif.framesPerSecond = 0;
            isCurrentlyPlaying = false;
        }
    }
}
