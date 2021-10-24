using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Image sleepingCover;

    public int points;
    public int money;

    private void Start()
    {
        OnGameLost += delegate { gameOverUI.SetActive(true); };
        OnDayEnded += delegate { dayFinishedUI.SetActive(false); };
        OnDiveEnd += delegate { dayFinishedUI.SetActive(true); };
    }

    public void RunCover()
    {
        StartCoroutine(CoverSleep());
    }

    public void RunRestartCover()
    {
        StartCoroutine(CoverGameRestart());
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
        if(money >= bed.price)
        {
            money -= bed.price;
        }
        else
        {
            return;
        }

        unlockedBeds.Add(bed);

        if(bed.energyFillAmount > ship.bed.energyFillAmount)
        {
            ship.bed = bed;
        }
    }

    public void UnlockSpinner(SpinnerObject spinner)
    {
        if(money >= spinner.price)
        {
            money -= spinner.price;
        }
        else
        {
            return;
        }

        unlockedSpinners.Add(spinner);

        if(spinner.level > ship.spinner.spinner.level)
        {
            ship.spinner.spinner = spinner;
        }
    }

    public IEnumerator CoverSleep()
    {
        //while (true)
        {
            while (sleepingCover.color.a < 1)
            {
                sleepingCover.color = new Color(sleepingCover.color.r, sleepingCover.color.g, sleepingCover.color.b, sleepingCover.color.a + 5 * Time.deltaTime);
                yield return null;
            }

            GameManager.Instance.EndDay();

            yield return new WaitForSeconds(0.3f);

            while (sleepingCover.color.a > 0)
            {
                sleepingCover.color = new Color(sleepingCover.color.r, sleepingCover.color.g, sleepingCover.color.b, sleepingCover.color.a - 5 * Time.deltaTime);
                yield return null;
            }
            yield break;
        }
    }

    public IEnumerator CoverGameRestart()
    {
        //while (true)
        {
            while (sleepingCover.color.a < 1)
            {
                sleepingCover.color = new Color(sleepingCover.color.r, sleepingCover.color.g, sleepingCover.color.b, sleepingCover.color.a + 5 * Time.deltaTime);
                yield return null;
            }

            GameManager.Instance.Restart();

            yield return new WaitForSeconds(0.5f);

            while (sleepingCover.color.a > 0)
            {
                sleepingCover.color = new Color(sleepingCover.color.r, sleepingCover.color.g, sleepingCover.color.b, sleepingCover.color.a - 5 * Time.deltaTime);
                yield return null;
            }
            yield break;
        }
    }
}
