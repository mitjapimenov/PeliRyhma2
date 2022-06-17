using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public bool onPause;
    public GameObject pauseMenuUI;
    public GameObject mainMenuUI;
    public string sceneName;
    public bool sceneCheck = false;



    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;

        onPause = gameIsPaused;
        if (sceneName == "Jape")
        {
            sceneCheck = true;
        }
        else
        {
            sceneCheck = false;
        }
        if (sceneCheck == true && Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
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
    public void Restart()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Jape");
        pauseMenuUI.SetActive(false);
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
}
