using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour{

    //Script for Ammo Pickup
    private PlayerController player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.maxAmmo += 20;
            player.UpdateAmmo();
            Destroy(gameObject);
        }
    }
}
