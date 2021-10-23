using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bed", menuName = "Game/Bed")]
public class BedObject : ScriptableObject
{
    public float energyFillAmount;
    public string description;
}
