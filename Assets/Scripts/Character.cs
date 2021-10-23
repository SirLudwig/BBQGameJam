using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Energy;

    private bool shouldLoseEnergy;

    private void Start()
    {
        GameManager.Instance.OnDiveStart += delegate { shouldLoseEnergy = true; };
        GameManager.Instance.OnDiveEnd += delegate { shouldLoseEnergy = false; };
        GameManager.Instance.OnGameLost += delegate { shouldLoseEnergy = false; };
    }

    private void Update()
    {
        if (shouldLoseEnergy)
        {
            Energy -= Time.deltaTime;
        }

        if(Energy <= 0)
        {
            GameManager.Instance.OnGameLost?.Invoke();
        }
    }
}
