using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Bed", menuName = "Game/Bed")]
public class BedObject : ScriptableObject
{
    public Sprite image;

    public float energyFillAmount;
    public string description;
    public int price;
}
