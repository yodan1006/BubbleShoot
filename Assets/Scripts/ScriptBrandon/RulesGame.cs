using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesGame : MonoBehaviour
{
    public int life  = 3;
    public int score { get; private set; }

    public int AddScore100()
    {
        int addScore = 100;
        score += addScore;
        return score;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
