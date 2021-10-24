using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public Button tryAgainButton;
    public Button quitButton;

    public TextMeshProUGUI pointsText;

    public void Start()
    {
        tryAgainButton.onClick.AddListener(delegate { GameManager.Instance.RunRestartCover(); });
        quitButton.onClick.AddListener(delegate { Application.Quit(); });
        GameManager.Instance.OnGameLost += delegate { pointsText.text = GameManager.Instance.points + " POINTS"; };
    }
}
