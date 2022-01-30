using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviourPunCallbacks
{

    private string _username;
    private string _mail;
    private string _pass;


    public void UpdateUserName(string username)
    {
        _username = username;
    }

    public void UpdateMail(string mail)
    {
        _mail = mail;
    }

    public void UpdatePass(string pass)
    {
        _pass = pass;
    }

    [SerializeField]
    private TMP_Text connectingText;

    public void CreateAccount()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _mail,
            Password = _pass,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, 
            result =>
            {
                Debug.Log("Success");
            }, error => {
                Debug.LogError($"Error: {error}");
            }
            );
    }

    public async void Login()
    {
        var request = new LoginWithPlayFabRequest
        {
            Username = "test",// _username,
            Password = "test22", //_pass,
        };

        // Имитация долгого коннекта
        connectingText.text = "Connecting...";
        await Task.Delay(2000);
        connectingText.text = "Connected. Enеtering the lobby...";
        await Task.Delay(1000);

        PlayFabClientAPI.LoginWithPlayFab(request,
            result =>
            {
                Debug.Log("Success Login");
                SceneManager.LoadScene("Lobby");
            }, error => {
                Debug.LogError($"Error: {error}");
            }
            );
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PlayFabSettings.staticSettings.TitleId = "6469A";
    }




    public void ConnectToPhoton()
    {
        if (PhotonNetwork.IsConnected)
        {
            //  PhotonNetwork.JoinRandomRoom();
            PhotonNetwork.Disconnect();

        }
        else
        {
        

        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Photon Success");
        connectingText.text = "";
    }

   

    //public void ConnectToPlayFab()
    //{
    
    //    if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
    //    {
    //        PlayFabSettings.staticSettings.TitleId = "6469A";
    //        Debug.Log("Title ID was installed");
    //    }
    //    PlayFabSettings.staticSettings.TitleId = "6469A";
    //    var request = new LoginWithCustomIDRequest { CustomId = "lesson3", CreateAccount = true };

    //    PlayFabClientAPI.LoginWithCustomID(request, OnPlayFabLoginSuccess, OnPlayFabLoginFailure);
    //}


    //public void BadConnectToPlayFab()
    //{


    //        PlayFabSettings.staticSettings.TitleId = "XXXXX";
    //        Debug.Log("BAD Title ID was installed");
       
    //    var request = new LoginWithCustomIDRequest { CustomId = "lesson3", CreateAccount = true };

    //    PlayFabClientAPI.LoginWithCustomID(request, OnPlayFabLoginSuccess, OnPlayFabLoginFailure);
    //}

    private void OnPlayFabLoginSuccess(LoginResult result)
    {
        Debug.Log("PlayFab Success");
        connectingText.text = "";

    }

    private void OnPlayFabLoginFailure(PlayFabError error)
    {
        Debug.LogError($"Fail: {error}");
        connectingText.text = "";

    }
}
