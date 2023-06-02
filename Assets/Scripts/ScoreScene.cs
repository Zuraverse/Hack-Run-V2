using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChnager : MonoBehaviour
{
    public void ShowScoreScene()
    {
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
    }

    public void NFTScene()
    {
        SceneManager.LoadScene("NFTFetcher");
    }

}
