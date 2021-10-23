using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelection : MonoBehaviour
{
    public static int GetRandomValue(List<Selection> selections)
    {
        float randomValue = Random.value;
        float currProbability = 0;
        
        foreach (Selection selection in selections)
        {
            currProbability += selection.Probability;

            if (currProbability >= randomValue)
            {
                return selection.GetValue();
            }
        }

        return -1;
    }
}

public class Selection
{
    private int _minValue;
    private int _maxValue;
    public float Probability;
    
    public Selection(int minValue, int maxValue, float probability)
    {
        this._minValue = minValue;
        this._maxValue = maxValue;
        this.Probability = probability;
    }

    public int GetValue()
    {
        return Random.Range(_minValue, _maxValue + 1);
    }
}



