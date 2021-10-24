using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterface : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timeText;
    
    void Update()
    {
        moneyText.SetText(GameManager.Instance.money.ToString());
        timeText.SetText(GameManager.Instance.ship.character.Energy.ToString("0.00"));
    }
}
