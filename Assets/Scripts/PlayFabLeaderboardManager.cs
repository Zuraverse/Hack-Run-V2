using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;



public class PlayFabLeaderboardManager : MonoBehaviour
{
    public Text positionText;
    public Text displayNameText;
    public Text statValueText;
    public Text leaderboardText;
    public Text CurrentPlayerpositionText;
    public Text CurrentPlayerdisplayNameText;
    public Text CurrentPlayerstatValueText;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Loginn", 2);
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

    void Update()
    {

    }

    public void Loginn()
    {
        //PlayerPrefs.SetString("myString", "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2fe");
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


    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnError);
    }

    void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard sent");

        for (int i = 0; i < 4; i++)
        {
            UpdateDisplayName();
            GetLeaderboard();
        }
    }


    public void UpdateDisplayName()
    {
        //PlayerPrefs.SetString("myString", "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2f");
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


    void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log("Leaderboard fetched completed");
        string positionString = "";
        string DisplayNameString = "";
        string statValueString = "";
        foreach (var item in result.Leaderboard)
        {
            int rank = item.Position + 1;
            positionString += "<color=#FF0000>" + rank + "</color>\n\n\n";
            DisplayNameString += item.DisplayName + "\n\n\n";
            statValueString += "<color=#0000FF>" + item.StatValue.ToString() + "</color>\n\n\n";
        }
        positionText.text = positionString;
        displayNameText.text = DisplayNameString;
        statValueText.text = statValueString;
    }


    public void GetLeaderboardIterate()
    {
        for(int i = 0; i < 4; i++)
        {
            GetLeaderboard();
        }
    }

    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest { StatisticName = "Score", MaxResultsCount = 1 };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardAroundPlayerFailure);
    }

    void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {
        string CurrentPlayerpositionString = "";
        string CurrentPlayerDisplayNameString = "";
        string CurrentPlayerstatValueString = "";
        foreach (var item in result.Leaderboard)
        {
            int rank = item.Position + 1;
            CurrentPlayerpositionString += "<color=#FF0000>" + rank + "</color>\n\n\n";
            CurrentPlayerDisplayNameString += item.DisplayName + "\n\n\n";
            CurrentPlayerstatValueString += "<color=#0000FF>" + item.StatValue.ToString() + "</color>\n\n\n";
        }
        CurrentPlayerpositionText.text = CurrentPlayerpositionString;
        CurrentPlayerdisplayNameText.text = CurrentPlayerDisplayNameString;
        CurrentPlayerstatValueText.text = CurrentPlayerstatValueString;
    }

    void OnGetLeaderboardAroundPlayerFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}


