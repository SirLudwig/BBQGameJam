using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spinner", menuName = "Game/Spinner")]
public class SpinnerObject : ScriptableObject
{
    public float minTemp;
    public float maxTemp;
    public AnimationCurve weightInfluence;
    public float coolOffPace;
}
