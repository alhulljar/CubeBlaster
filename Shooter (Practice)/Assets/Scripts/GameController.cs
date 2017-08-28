using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    //Game Properties
    public GUIText scoreText;
    public GameObject pauseMenu;
    public PlayerController playerController;
    public EnemySpawner enemySpawner;
    public int score;
    public int pointValue;
    public bool paused = false;
    
    //Coroutine begins countdown to game start
    IEnumerator CountDown()
    {
        //Disables enemy spawning and player movement until end of countdown
        int count = 3;
        enemySpawner.enabled = false;
        playerController.enabled = false;
        for (int i = 0; i < 3; i++)
        {
            playerController.eventText.text = count + "...";
            yield return new WaitForSeconds(1f);
            count--;
            
        }
        playerController.eventText.text = "Start!";
        enemySpawner.FirstWave();
        enemySpawner.enabled = true;
        playerController.enabled = true;
        yield return new WaitForSeconds(1f);
        playerController.eventText.text = "";
    }


	//Sets starting score to zero, gets references and starts Countdown corountine
	void Start ()
    {

        score = 0;
        UpdateScore();
        pauseMenu.SetActive(false);
        enemySpawner = GetComponent<EnemySpawner>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(CountDown());
        StopCoroutine(CountDown());
    }

    //Checks if game is paused 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                paused = true;
                playerController.canFire = false;
            }
            else if (paused)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                paused = false;
                playerController.canFire = true;
            }
        }
    }

    //Unpauses Game via pause menu button
    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        paused = false;
        playerController.canFire = true;
    }

    //Restarts and Reloads game if button is pressed
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Unpause();
    }

    //Adds points to current Score
    public void AddScore()
    {
        score += pointValue / 2;
        UpdateScore();
    }
	
    //Keeps track of and updates current Player score
    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateHiScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

}
