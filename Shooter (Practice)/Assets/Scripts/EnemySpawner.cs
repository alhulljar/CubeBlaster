using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    //Spawner Properties
    public GameObject[] baddyType;
    public float spawnRate;
    public int enemyCap = 15;
    private int type;
    public float curEnemyCap; 
    private Vector3 position;
    private bool spawning = false;

    //Function checks if enemyCap has been reached and cancels spawning if it has
    void SpawnEnemy()
    { 
        if(curEnemyCap == enemyCap)
        {
            CancelInvoke();
            spawning = false;
            return;
        }

        //Randomizes Enemy Position
        type = Random.Range(0, baddyType.Length);
        position = new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f)); 
        Instantiate(baddyType[type], position, Quaternion.identity);
        curEnemyCap++;

    }

    //Calls function that spawns enemies repeatedly at the spawnRate
    public void EnemyRespawn()
    {
        InvokeRepeating("SpawnEnemy", 3f, spawnRate);
    }

    //Checks if the number of enemies in game is low enough to spawn more
    void Update()
    {      
        if(spawning == false && curEnemyCap < 10)
        {
            spawning = true;
            EnemyRespawn();
        }
    }

    //Quick Spawns a wave of enemies when countdown is over
    public void FirstWave()
    {
        for (int i = 0; i < enemyCap; i++)
        {
            //Spawns random baddy at random position
            type = Random.Range(0, baddyType.Length);
            position = new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f));
            switch (type)
            {
                case 0:
                    Instantiate(baddyType[type], position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(baddyType[type], position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(baddyType[type], position, Quaternion.identity);
                    break;
                default:
                    Instantiate(baddyType[type], position, Quaternion.identity);
                    break;
            }

        }

        curEnemyCap = enemyCap;
    }


}
