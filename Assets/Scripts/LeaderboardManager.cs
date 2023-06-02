using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Xml.Serialization;
using System.Data.Common;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Diagnostics.CodeAnalysis;

public class LeaderboardManager : MonoBehaviour
{
    public Text leaderboardText;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Loginn", 5);
    }

    void OnSucces(LoginResult result)
    {
        Debug.Log("Congrats Your Account is created");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Failure");
        Debug.LogError(error.GenerateErrorReport());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Loginn()
    {
        //PlayerPrefs.SetString("myString", PlayerPrefs.GetString("WalletAddress"));
        string walletAdress = PlayerPrefs.GetString("WalletAddress");
        Debug.Log(walletAdress);
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "9EF26";
        }

        var request = new LoginWithCustomIDRequest
        {
            CustomId = walletAdress,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);

    }

    public void SendLeaderboardd(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate { StatisticName = "Score", Value = score }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnError);
    }

    //public void GetLeaderboard()
    //{
    //    var request = new GetLeaderboardRequest
    //    {
    //        StatisticName = "Score",
    //        StartPosition = 0,
    //        MaxResultsCount = 10
    //    };
    //    PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnError);
    //}

    void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard sent");
        //UpdateDisplayName();
        //GetLeaderboard();
    }


    public void UpdateDisplayName()
    {
        PlayerPrefs.SetString("myString", "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2f");
        string walletAdres = PlayerPrefs.GetString("WalletAddress");
        if (!PlayerPrefs.HasKey("WalletAddress"))
        {
            PlayerPrefs.SetString("WalletAddress", walletAdres);
        }
        string name = walletAdres.Substring(0, 4) + "..." + walletAdres.Substring(walletAdres.Length - 4);
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdateSuccess, OnError);
    }


    void OnDisplayNameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Display name updated successfully");
    }


    //void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    //{
    //    Debug.Log("Leaderboard fetched completed");
    //    string leaderboardString = "";
    //    foreach (var item in result.Leaderboard)
    //    {
    //        leaderboardString += item.Position + "              " + item.DisplayName + "           " + item.StatValue + "\n";
    //    }
    //    leaderboardText.text = leaderboardString;
    //}

}