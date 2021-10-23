using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public Action OnGameLost;
    public Action OnDayEnded;

    public void EndDay()
    {
        OnDayEnded?.Invoke();
    }
}
