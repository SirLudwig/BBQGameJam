using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public SpinnerObject spinner;

    public Net net;

    public float currentTemp;
    public float currentWeight;
    public float pullingSpeed;

    private bool dived;
    public float maxHeight;
    private bool isLocked = false;

    private void Start()
    {
        ResetValues();
        maxHeight = net.GetPosition().y;

        GameManager.Instance.OnDiveEnd += delegate { Lock(); };
        GameManager.Instance.OnGameLost += delegate { Lock(); };
        GameManager.Instance.OnDayEnded += delegate { Unlock(); };
    }

    private void Update()
    {
        if(isLocked)
        {
            net.PullingSpeed = 0f;
            return;
        }

        if(Mathf.Abs(maxHeight - net.GetPosition().y) > 1)
        {
            if (dived == false)
            {
                GameManager.Instance.OnDiveStart?.Invoke();
                dived = true;
                Debug.Log("Dive start");
            }
        }
        else if(Mathf.Abs(maxHeight - net.GetPosition().y) < 0.2f)
        {
            if (dived == true)
            {
                GameManager.Instance.OnDiveEnd?.Invoke();
                dived = false;
                Debug.Log("Dive end");
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            Lower();
        }
        else
        {
            pullingSpeed = (1 - spinner.temperatureInfluence.Evaluate(Mathf.InverseLerp(spinner.minTemp, spinner.maxTemp, currentTemp))) * spinner.defaultPullingSpeed;
            if (Input.GetKey(KeyCode.W) && net.GetPosition().y < maxHeight)
            {
                Pull();
            }
            else
            {
                Cool();
            }
        }
        
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public void ResetValues()
    {
        currentTemp = spinner.minTemp;
        currentWeight = 0;
    }

    public void UpdateSpinner(SpinnerObject newSpinner)
    {
        spinner = newSpinner;
    }

    public void Pull()
    {
        net.EnableCollisions();
        currentTemp += currentWeight * spinner.heatupMultiplier * Time.deltaTime;
        if(currentTemp > spinner.maxTemp)
        {
            currentTemp = spinner.maxTemp;
        }

        net.PullingSpeed = pullingSpeed;
    }

    public void Lower()
    {
        net.DisableCollisions();
        net.PullingSpeed = -spinner.loweringSpeed;
    }

    public void Cool()
    {
        net.PullingSpeed = 0;

        currentTemp -= spinner.coolingCurve.Evaluate(Mathf.InverseLerp(spinner.minTemp, spinner.maxTemp, currentTemp)) * spinner.coolOffPace * Time.deltaTime;

        if (currentTemp < spinner.minTemp)
        {
            currentTemp = spinner.minTemp;
        }
    }

}
