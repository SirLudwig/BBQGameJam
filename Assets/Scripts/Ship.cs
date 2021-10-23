using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Character character;

    public BedObject bed;

    public Spinner spinner;

    private void Start()
    {
        GameManager.Instance.OnDayEnded += RefillCharacterEnergy;
        GameManager.Instance.OnDayEnded += spinner.ResetValues;

        RefillCharacterEnergy();
    }

    public void RefillCharacterEnergy()
    {
        character.Energy = bed.energyFillAmount;
    }
}
