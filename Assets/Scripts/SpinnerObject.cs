using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spinner", menuName = "Game/Spinner")]
public class SpinnerObject : ScriptableObject
{
    public float defaultPullingSpeed;
    public float loweringSpeed;
    public float minTemp;
    public float maxTemp;
    public float heatupMultiplier;
    public AnimationCurve temperatureInfluence;
    public AnimationCurve coolingCurve;
    public float coolOffPace;
}
