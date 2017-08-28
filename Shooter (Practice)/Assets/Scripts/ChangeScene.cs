using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{

    public GameController gameController;

    //Gets references
    void Start()
    {
        gameController = GetComponent<GameController>();        
    }

    //Changes Game Scene
    public void SceneChange(int changeToScene)
    {
        SceneManager.LoadScene(changeToScene);
        gameController.paused = false;
        Time.timeScale = 1;
        gameController.pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
	
}
