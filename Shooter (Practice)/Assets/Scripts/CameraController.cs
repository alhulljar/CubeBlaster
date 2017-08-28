using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    //Camera Properties
    public Transform player;
    public PlayerController controller;
    public Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        transform.rotation = player.rotation;
        transform.position = player.position + offset;
    }

   
	void LateUpdate ()
    {
        transform.rotation = player.rotation;
        transform.position = player.position + offset;
    }

    
}
