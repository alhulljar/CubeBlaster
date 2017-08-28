using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour {

    //More or less same deal as EnemySpawner
    public GameObject[] PickType;
    public float spawnRate;
    public int pickupCap = 5;
    private int type;
    public float curPickupCap;
    private Vector3 position;
    private bool spawning = false;

    //Function checks if PickUpCap has been reached and cancels spawning if it has
    void SpawnPickUp()
    {
        if (curPickupCap == pickupCap)
        {
            CancelInvoke();
            spawning = false;
            return;
        }

        //Randomizes Pickup Position
        type = Random.Range(0, PickType.Length);
        position = new Vector3(Random.Range(-50f, 50f), 1.0f, Random.Range(-50f, 50f));
        Instantiate(PickType[type], position, Quaternion.identity);
        curPickupCap++;

    }

    //Respawns Pickups
   void PickUpRespawn()
    {
        InvokeRepeating("SpawnPickUp", 3f, spawnRate);
    }

    //Checks to see if should start spawning Pickups again
    void Update()
    {
        if (spawning == false && curPickupCap < 2)
        {
            spawning = true;
            PickUpRespawn();
        }
    }
}
