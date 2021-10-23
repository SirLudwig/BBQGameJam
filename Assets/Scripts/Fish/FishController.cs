using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private FishStatistics _stats;
    private float _speed;
    private float _value;

    private SpriteRenderer _spriteRenderer;

    public void SetStats(FishStatistics stats)
    {
        _stats = stats;
        CustomizateFish();
    }

    private void CustomizateFish()
    {
        gameObject.name = _stats.Name;
        GetComponent<SpriteRenderer>().sprite = _stats.FishSprite;
    }

    private void Start()
    { 
        
        
        
    }
    

    private void MoveTo(float targetXPosition)
    {
        transform.LeanMoveX(targetXPosition, _speed);
    }
}
