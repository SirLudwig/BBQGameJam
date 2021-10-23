using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Spinner", menuName = "Game/Spinner")]
public class SpinnerObject : ScriptableObject
{
    public int level;

    public Sprite image;

    public float defaultPullingSpeed;
    public float loweringSpeed;
    public float minTemp;
    public float maxTemp;
    public float heatupMultiplier;
    public AnimationCurve temperatureInfluence;
    public AnimationCurve coolingCurve;
    public float coolOffPace;

    public string description;
}
