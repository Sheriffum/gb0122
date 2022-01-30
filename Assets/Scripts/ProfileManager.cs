using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;

public class ProfileManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text loginText;
    // Start is called before the first frame update
    void Start()
    {
         PlayFabClientAPI.GetAccountInfo(new PlayFab.ClientModels.GetAccountInfoRequest(),
            success => {
                loginText.text = $"Welcome, {success.AccountInfo.Username}";
            },

            errorCallback =>
            {
                loginText.text = errorCallback.ErrorMessage;
            }
            );

        
    }

}
