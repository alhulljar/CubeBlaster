using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //Reference for enemy physics
    private Rigidbody rb;

    //References to Player, EnemySpawner, and PlayerController
    public Transform playerPos;
    public PlayerController playerController;
    public EnemySpawner spawner;


    //Enemy Properties
    public float moveSpeed;
    public int pushBack;
    public int enemyDmg;
    public int value;
    public int type;

    //Gets References
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawner>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    //Causes enemy to constantly chase Player
    void Update ()
    {
        transform.LookAt(playerPos);
        //this.transform.Translate(0, 0, moveSpeed / 100);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed / 10);
    }

    //Detects when enemy hits player and deals damage
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Causes enemy to get pushbacked after dealing dmg
            rb.AddForce(Vector3.up * pushBack);
            if (playerController.canHit == true)
            {
                playerController.health = playerController.health - enemyDmg;
                playerController.UpdateHealth();
            }
        }
    }
}
