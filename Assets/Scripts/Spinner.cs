using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField]
    private SpinnerObject spinner;

    [SerializeField]
    private Net net;

    public float currentTemp;
    public float currentWeight;
    public float pullingSpeed;

    private void Start()
    {
        ResetValues();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Lower();
        }
        else
        {
            pullingSpeed = (1 - spinner.temperatureInfluence.Evaluate(Mathf.InverseLerp(spinner.minTemp, spinner.maxTemp, currentTemp))) * spinner.defaultPullingSpeed;
            if (Input.GetKey(KeyCode.W))
            {
                Pull();
            }
            else
            {
                Cool();
            }
        }
        
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
        //net.DisableCollisions();
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
