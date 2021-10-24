using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifDisplayer : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] frames; 
    public float framesPerSecond = 10.0f;

    public Spinner spinner;

    public float startTime;
    public float previousEndTime;

    public int currentIndex = 0;

    public IEnumerator PlayGif()
    {
        while(framesPerSecond != 0)
        {
            if(currentIndex >= frames.Length)
            {
                currentIndex = 0;
            }
            targetImage.sprite = frames[currentIndex];
            currentIndex++;
            yield return new WaitForSeconds(0.2f + Mathf.InverseLerp(spinner.spinner.defaultPullingSpeed, 0, spinner.pullingSpeed));
        }
    }

}
