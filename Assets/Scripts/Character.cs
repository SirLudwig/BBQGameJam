using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Energy { get; set; }

    private void Update()
    {
        if(Energy <= 0)
        {
            GameManager.Instance.OnGameLost?.Invoke();
        }
    }
}
