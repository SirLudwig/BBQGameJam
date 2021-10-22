using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField]
    SpinnerObject spinner;

    public float currentTemp;
    public float currentWeight;

    public void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Pull();
        }
        else
        {
            Cool();
        }
    }

    public void Pull()
    {
        currentTemp += spinner.weightInfluence.Evaluate(Mathf.Clamp(currentTemp, spinner.minTemp, spinner.maxTemp) * currentWeight * Time.deltaTime);
    }

    public void Cool()
    {
        if(currentTemp > 1)
        {
            currentTemp -= spinner.coolOffPace * Time.deltaTime;
        }
    }

}
