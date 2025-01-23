using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitManager : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void LoadScene(string MainMenu)
    {
        SceneManager.LoadScene(MainMenu);
        Time.timeScale = 1f;
    }
}
