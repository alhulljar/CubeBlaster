﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private bool jump = true;
    public int maxAmmo = 50;

    //Player Properties
    public int moveSpeed;
    public int turnSpeed;
    public int jumpStrength;
    public int health;
    public int ammo;
    public float untouchable;
    public float reloadTime;

  

    public bool canHit = true;
    public bool canFire = true;

    public GUIText healthText;
    public GUIText ammoText;
    public GUIText eventText;

    //Reference to Player physics, Game Controller, gun, and projectile
    public GameObject bullet;
    public Transform launcher;
    public Rigidbody rb;
    private GameController gc;


    //Gets references and sets starting health and ammo
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        UpdateHealth();
        UpdateAmmo();
        eventText.text = "";
    }

    //Moving and Shooting
    void Update ()
    {
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * turnSpeed * Time.deltaTime);         
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canFire == true)
            {                       
                Instantiate(bullet, launcher.position, launcher.rotation);              
                ammo--;
                UpdateAmmo();
            }
               
        }
        //Checks if Player should be dead
        if (health <= 0)
        {
            Death();
        }
    }

    //Controls jump and changes jump boolean to false
    void FixedUpdate()
    {
        
        if (jump == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpStrength);
            jump = false;
        }

    }

    //Enables jump again after touching floor
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            jump = true;
        }
    }

    //After being hit makes enemies wait a number of seconds before hitting again
    IEnumerator Invincible()
    {
        canHit = false;
        yield return new WaitForSeconds(untouchable);
        canHit = true;
        
    }
    //Reloads Gun
    IEnumerator Reload()
    {
        eventText.text = "Reloading...";
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        canFire = true;
        eventText.text = "";
        UpdateAmmo();
    }

    //Keeps track of current weapon ammo
    void UpdateAmmo()
    {
        ammoText.text = "Ammo: " + ammo;
        if (ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    //Keeps track of current Health and Calls to Invincible Coroutine on Hit
    public void UpdateHealth()
    {
        healthText.text = "Health: " + health;
        StartCoroutine(Invincible());
    }

    //Called when player Health is depleted
    void Death()
    {
        gc.UpdateHiScore();
        SceneManager.LoadScene(2);
    }

}
