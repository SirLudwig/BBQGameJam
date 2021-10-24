using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchasableItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private TextMeshProUGUI priceText;

    public Image image;
    public Button button;

    public string Description
    {
        get
        {
            return descriptionText.text;
        }
        set
        {
            descriptionText.SetText(value);
        }
    }

    public int Price
    {
        set
        {
            priceText.text = value.ToString() + " PLN";
        }
    }
}
