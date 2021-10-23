using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new fish stats", menuName = "Fish Stats")]
public class FishStatistics : ScriptableObject
{
    public Sprite FishSprite;
    public string Name;
    public int Value;
    public float Speed;
    public float Weight;
}
