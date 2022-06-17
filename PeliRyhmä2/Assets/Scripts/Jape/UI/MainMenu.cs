using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public GameObject mainMenuUI;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    private void Update()
    {
        if(sceneName == "MainMenu")
        {
            Debug.Log("SceneName on MainMenu");
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Jape");
        mainMenuUI.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
