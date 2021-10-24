using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public Transform bedRoot;
    public Transform spinnerRoot;
    public PurchasableItem itemPrefab;

    List<PurchasableItem> spawnedItems;

    void Start()
    {
        spawnedItems = new List<PurchasableItem>();
        Refresh();
    }

    private void Refresh()
    {
        for (int i = spawnedItems.Count - 1; i >= 0; i--)
        {
            Destroy(spawnedItems[i].gameObject);
        }

        spawnedItems.Clear();

        foreach (BedObject bed in GameManager.Instance.allAvailableBeds)
        {
            {
                PurchasableItem item = Instantiate(itemPrefab, bedRoot);

                item.image.sprite = bed.image;
                item.Description = bed.description;
                item.Price = bed.price;
                item.button.onClick.AddListener(delegate { GameManager.Instance.UnlockBed(bed); Refresh(); });

                if(GameManager.Instance.unlockedBeds.Contains(bed))
                {
                    item.cover.SetActive(true);
                }

                spawnedItems.Add(item);
            }
        }

        foreach (SpinnerObject spinner in GameManager.Instance.allAvailableSpinners)
        {
            {
                PurchasableItem item = Instantiate(itemPrefab, spinnerRoot);

                item.image.sprite = spinner.image;
                item.Description = spinner.description;
                item.Price = spinner.price;
                item.button.onClick.AddListener(delegate { GameManager.Instance.UnlockSpinner(spinner); Refresh(); });

                if(GameManager.Instance.unlockedSpinners.Contains(spinner))
                {
                    item.cover.SetActive(true);
                }

                spawnedItems.Add(item);
            }
        }
    }
}
