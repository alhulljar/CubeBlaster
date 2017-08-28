using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Transform playerPos;
    
    //Enemy Properties
    public float moveSpeed;
    public int pushBack;
    public int enemyDmg;
    public int value;
    public int type;

    //Reference for enemy physics
    private Rigidbody rb;

    //References to Player, and PlayerController
    private PlayerController playerController;
 


    //Gets References
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    //Causes enemy to constantly chase Player
    void Update ()
    {
        transform.LookAt(playerPos);
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
