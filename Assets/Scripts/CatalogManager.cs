using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogManager : MonoBehaviour
{


    private readonly Dictionary<string, CatalogItem> _catalog = new Dictionary<string, CatalogItem>();

    [SerializeField]
    private GameObject ItemContainer;

    [SerializeField]
    GridLayoutGroup itemsGrid;

    // Start is called before the first frame update
    void Start()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogSuccess, OnFailure);
    }

    private void OnFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }

    private void OnGetCatalogSuccess(GetCatalogItemsResult result)
    {
        HandleCatalog(result.Catalog);
        Debug.Log($"Catalog was loaded successfully!");
    }

    private void HandleCatalog(List<CatalogItem> catalog)
    {
        foreach (var item in catalog)
        {
            _catalog.Add(item.ItemId, item);
            if (item.VirtualCurrencyPrices != null)
            {
                if (item.VirtualCurrencyPrices.ContainsKey("GD"))
                {
                    var catalogItem = Instantiate(ItemContainer, itemsGrid.transform);
                    catalogItem.GetComponent<CatalogItemElement>().Initialize(item);
                    catalogItem.transform.SetParent(itemsGrid.transform);
                    Debug.Log($"Catalog item {item.ItemId} was added successfully!");
                }
            } else
            {
                Debug.Log($"Catalog item {item.ItemId} skipped! No prices");
            }


        }
    }

}
