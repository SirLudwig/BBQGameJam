using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Button tryAgainButton;
    public Button quitButton;

    public void Start()
    {
        tryAgainButton.onClick.AddListener(delegate { GameManager.Instance.Restart(); });
        quitButton.onClick.AddListener(delegate { Application.Quit(); });
    }
}
