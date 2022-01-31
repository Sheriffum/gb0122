using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatalogItemElement : MonoBehaviour
{
    [SerializeField]
    private TMP_Text itemNameText;

    [SerializeField]
    private TMP_Text priceText;

    [SerializeField]
    private Button buyButton;

    public void Initialize(CatalogItem item)
    {
        itemNameText.text = item.DisplayName;
        if (item.VirtualCurrencyPrices != null)
        {
            if (item.VirtualCurrencyPrices.ContainsKey("GD"))
            {
                priceText.text = item.VirtualCurrencyPrices["GD"].ToString() + " G";
                buyButton.onClick.AddListener(()=>ByuItem(item)); 
          
            } else
            {
                buyButton.gameObject.SetActive(false);
            }
        }
        



    }

    private void ByuItem(CatalogItem item)
    {
        var balance = 0;

        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), success =>
        {
            balance = success.VirtualCurrency["GD"];

            if (balance >= item.VirtualCurrencyPrices["GD"])
            {
                Debug.Log($"You bought a {item.DisplayName}");
            } else
            {
                Debug.Log("Not enough money");
            }
            //    
        }, error => {
            Debug.LogError($"{error.ErrorDetails}");
        });

    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
