using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public bool gameIsPaused = false;
    
    public GameObject pauseMenuUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) 
            || Input.GetKeyDown(KeyCode.P))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
