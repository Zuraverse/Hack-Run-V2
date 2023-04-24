using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;
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

    public void SendLeaderboard(int score){
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "HackRunLeaderboard",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Successfull leaderboard sent");
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
        leaderboardString += item.Position + ". " + item.PlayFabId + " - " + item.StatValue + "\n";
    }
    leaderboardText.text = leaderboardString;
}

}
