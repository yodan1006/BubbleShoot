using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesGame : MonoBehaviour
{
    public int life = 3;
    public int score { get; private set; }
    
    private int maxHighScores = 5;
    
    public int AddScore100()
    {
        int addScore = 100;
        score += addScore;
        return score;
    }
    
    public int AddScore500()
    {
        int addScore = 500;
        score += addScore;
        return score;
    }
    
    public void GameOver()
    {
        CheckAndAddHighScore(score);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private void CheckAndAddHighScore(int playerScore)
    {
        int[] highScores = LoadHighScores();
        
        for (int i = 0; i < maxHighScores; i++)
        {
            if (playerScore > highScores[i])
            {
                for (int j = maxHighScores - 1; j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                }
                
                highScores[i] = playerScore;
                
                SaveHighScores(highScores);
                return;
            }
        }
    }
    
    private int[] LoadHighScores()
    {
        int[] highScores = new int[maxHighScores];
        for (int i = 0; i < maxHighScores; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
        }
        return highScores;
    }
    
    private void SaveHighScores(int[] highScores)
    {
        for (int i = 0; i < maxHighScores; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
        PlayerPrefs.Save();
    }
}
