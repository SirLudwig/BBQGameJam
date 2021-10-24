using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public SpinnerObject spinner;
    public SpinnerUI spinnerUI;

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
        GameManager.Instance.OnDiveEnd += delegate { DestroyFishInNet(); };
        GameManager.Instance.OnDayEnded += delegate { Unlock(); };
    }

    private void Update()
    {
        spinnerUI.UpdateColor(spinner.temperatureInfluence.Evaluate(Mathf.InverseLerp(spinner.minTemp, spinner.maxTemp, currentTemp)));

        if(isLocked)
        {
            net.PullingSpeed = 0f;
            return;
        }

        currentWeight = net.GetTotalMass();

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
            if (Input.GetKey(KeyCode.W) && Mathf.Abs(maxHeight - net.GetPosition().y) > 0.2f)
            {
                Pull();
            }
            else
            {
                Cool();
            }
        }
        
    }

    public void DestroyFishInNet()
    {
        Debug.Log("Fish found: " + net.fishInNet.Count);
        for(int i = net.fishInNet.Count - 1; i >= 0; i--)
        {
            GameManager.Instance.money += net.fishInNet[i]._stats.Value;
            GameManager.Instance.points += net.fishInNet[i]._stats.Value;
            Destroy(net.fishInNet[i].gameObject);
        }
    }
    public void Lock()
    {
        isLocked = true;
        spinnerUI.StopPlay();
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
        spinnerUI.StartPlay();
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
        spinnerUI.StopPlay();
        net.PullingSpeed = 0;

        currentTemp -= spinner.coolingCurve.Evaluate(Mathf.InverseLerp(spinner.minTemp, spinner.maxTemp, currentTemp)) * spinner.coolOffPace * Time.deltaTime;

        if (currentTemp < spinner.minTemp)
        {
            currentTemp = spinner.minTemp;
        }
    }

}
