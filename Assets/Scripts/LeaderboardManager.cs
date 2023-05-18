using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class LeaderboardManager : MonoBehaviour
{
    private void Start()
    {
        Login("sawan");
    }
    public void Login(string customId)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successfully logged in to PlayFab");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Failed to log in to PlayFab: " + error.GenerateErrorReport());
    }

    public void UpdatePlayerScore(int score)
    {
        string customId = PlayerPrefs.GetString("WalletAddress");
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate { StatisticName = "HackRun", Value = score }
            },
            CustomTags = new Dictionary<string, string>
            {
                { "CustomId", customId }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatePlayerScoreSuccess, OnUpdatePlayerScoreFailure);
    }

    private void OnUpdatePlayerScoreSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully updated player score");
    }

    private void OnUpdatePlayerScoreFailure(PlayFabError error)
    {
        Debug.LogError("Failed to update player score: " + error.GenerateErrorReport());
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HackRun",
            StartPosition = 0,
            MaxResultsCount = 10,
            ProfileConstraints = new PlayerProfileViewConstraints
            {
                ShowDisplayName = true,
                ShowTags = true
            }
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            int rank = item.Position + 1;
            string name = item.DisplayName;
            //string customId = item.Profile.Tags["CustomId"];
            Debug.Log(rank + " " + name);
        }
    }

    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError("Failed to get leaderboard: " + error.GenerateErrorReport());
    }
}
