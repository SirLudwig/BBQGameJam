using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchasableItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    public Image image;
    public Button button;

    public string Description
    {
        get
        {
            return text.text;
        }
        set
        {
            text.SetText(value);
        }
    }
}
