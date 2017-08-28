using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    //Bullet Speed
    public int moveSpeed;

    //References to Scripts
    public EnemySpawner spawner;
    public GameController gameController;
    public EnemyMovement enemyProperties;
    private int value;
    
    //Gets References
   void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawner>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        Destroy(gameObject, 3f);
	}

    void OnTriggerEnter(Collider other)
    {
        //Destroys Enemy on Hit
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyProperties = other.gameObject.GetComponent<EnemyMovement>();
            value = enemyProperties.value;
            Destroy(other.gameObject);
            spawner.curEnemyCap -= 0.5f;
            gameController.pointValue = value;
            gameController.AddScore();
        }
       
        Destroy(gameObject);
    }
}
