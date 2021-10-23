using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Action OnGameLost;
    public Action OnDayEnded;
    public Action OnDiveStart;
    public Action OnDiveEnd;

    public Ship ship;

    public List<BedObject> allAvailableBeds;
    public List<SpinnerObject> allAvailableSpinners;

    public List<BedObject> unlockedBeds;
    public List<SpinnerObject> unlockedSpinners;

    public GameObject gameOverUI;
    public GameObject dayFinishedUI;

    public int points;
    public int money;

    private void Start()
    {
        OnGameLost += delegate { gameOverUI.SetActive(true); };
        OnDayEnded += delegate { dayFinishedUI.SetActive(false); };
        OnDiveEnd += delegate { dayFinishedUI.SetActive(true); };
    }

    public void Restart()
    {
        points = 0;
        money = 0;
        gameOverUI.SetActive(false);
        unlockedBeds.Clear();
        unlockedSpinners.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        EndDay();
    }

    public void EndDay()
    {
        OnDayEnded?.Invoke();
    }

    public void UnlockBed(BedObject bed)
    {
        unlockedBeds.Add(bed);

        if(bed.energyFillAmount > ship.bed.energyFillAmount)
        {
            ship.bed = bed;
        }
    }

    public void UnlockSpinner(SpinnerObject spinner)
    {
        unlockedSpinners.Add(spinner);

        if(spinner.level > ship.spinner.spinner.level)
        {
            ship.spinner.spinner = spinner;
        }
    }
}
