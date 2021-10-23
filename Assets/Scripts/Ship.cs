using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private BedObject bed;

    [SerializeField]
    private Spinner spinner;

    private Character character;

    private void Start()
    {
        GameManager.Instance.OnDayEnded += RefillCharacterEnergy;
        GameManager.Instance.OnDayEnded += spinner.ResetValues;
    }

    public void RefillCharacterEnergy()
    {
        character.Energy = bed.energyFillAmount;
    }
}
