using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject napisy;
    public Button startButton;
    public Button quitButton;

    public GameObject tutorialScreen;
    public Button continueTutorialButton;

    private void Start()
    {
        startButton.onClick.AddListener(() => { tutorialScreen.SetActive(true); napisy.SetActive(false); });
        continueTutorialButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        quitButton.onClick.AddListener(() => Application.Quit());
    }
}
