using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public GUIText hiScore;

    private int numZeros;

    //Called when Player Health reaches 0 and displays HighScore
    void Start()
    {
        numZeros = 9 - PlayerPrefs.GetInt("HighScore", 0).ToString().Length;
        for (int i = 0; i < numZeros; i++)
        {
            hiScore.text = hiScore.text + 0;
        }
        hiScore.text = hiScore.text + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    //Waits for Key Press to Either Restart or Clear HighScore
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }
}
