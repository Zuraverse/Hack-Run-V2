using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;

    //[SerializeField] private Text WalletText;
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    void Login(){
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    void OnSuccess(LoginResult result){
        Debug.Log("Success login/account creat!");
    }

    void OnError(PlayFabError error){
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {
                StatisticName = "HackRunLeaderboard",
                Value = score
            }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully updated leaderboard");

        // Update the high score for the current player
        var request = new GetPlayerStatisticsRequest
        {
            StatisticNames = new List<string> { "HackRunLeaderboard" }
        };
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetPlayerStatistics, OnError);
    }

    void OnGetPlayerStatistics(GetPlayerStatisticsResult result)
    {
        int highScore = 0;

        // Find the "HackRunLeaderboard" statistic and get the highest score
        foreach (var stat in result.Statistics)
        {
            if (stat.StatisticName == "HackRunLeaderboard" && stat.Value > highScore)
            {
                highScore = (int)stat.Value;
            }
        }

        // Update the high score for the current player
        var updateRequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> {
            { "HighScore", highScore.ToString() }
        }
        };
        PlayFabClientAPI.UpdateUserData(updateRequest, OnUpdateUserData, OnError);
    }

    void OnUpdateUserData(UpdateUserDataResult result)
    {
        Debug.Log("Successfully updated user data");
    }

    public void GetLeaderboard() {
        var request =new GetLeaderboardRequest {
            StatisticName = "HackRunLeaderboard",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

void OnLeaderboardGet(GetLeaderboardResult result){
        string leaderboardString = "";
        foreach(var item in result.Leaderboard) {
            leaderboardString += item.Position + "              " + PlayerPrefs.GetString("WalletAddress") + "           " + item.StatValue + "\n";
        }
        leaderboardText.text = leaderboardString;
    }

}
