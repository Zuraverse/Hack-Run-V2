using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScene : MonoBehaviour
{
    public void ShowScoreScene()
    {
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
    }


    public void BackToMain()
    {
        SceneManager.LoadScene("SRGMenu");
    }
}
