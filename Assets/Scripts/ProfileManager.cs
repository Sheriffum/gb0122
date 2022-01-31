using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;

public class ProfileManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text loginText;
    [SerializeField]
    private TMP_Text balanceText;
    // Start is called before the first frame update
    void Start()
    {
         PlayFabClientAPI.GetAccountInfo(new PlayFab.ClientModels.GetAccountInfoRequest(),
            success => {

                var balance = 0;

                PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), success=>
                {
                    balance = success.VirtualCurrency["GD"];
                    balanceText.text = $"Balance: {balance} G";
                //    
                }, error => {
                    Debug.LogError($"{error.ErrorDetails}");
                });

                loginText.text = $"Welcome, {success.AccountInfo.Username}";
            },

            errorCallback =>
            {
                loginText.text = errorCallback.ErrorMessage;
            }
            );

        
    }

}
