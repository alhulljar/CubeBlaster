using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    //Player Properties
    public int moveSpeed;
    public int turnSpeed;
    public int jumpStrength;
    public int health;
    public int ammo;
    public int maxAmmo = 50;

    public float untouchable;
    public float reloadTime;

    public GameObject bullet;
    public GameObject[] weapons;
    public Transform[] shotSpawns;

    [HideInInspector]
    public bool canFire = true, canHit = true;

    public int weaponNum = 0;
    private bool jump = true;
    public bool akimbo = false;

    //Reference to Player physics, and Game Controller
    private Rigidbody rb;
    private GameController gc;


    //Gets references and sets starting health and ammo
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        UpdateHealth();
        UpdateAmmo();
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
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            WeaponSwitch();
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
        gc.eventText.text = "Reloading...";
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        canFire = true;
        gc.eventText.text = "";
        UpdateAmmo();
    }

    //Keeps track of current weapon ammo
    void UpdateAmmo()
    {
        gc.ammoText.text = "Ammo: " + ammo;
        if (ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    //Keeps track of current Health and Calls to Invincible Coroutine on Hit
    public void UpdateHealth()
    {
        gc.healthText.text = "Health: " + health;
        StartCoroutine(Invincible());
    }

    //Called when player Health is depleted
    void Death()
    {
        gc.UpdateHiScore();
        SceneManager.LoadScene(2);
    }

    void WeaponSwitch()
    {
        if (weapons[1].activeInHierarchy)
        {
            weapons[1].SetActive(false);
            akimbo = false;
        }
        else
        {
            weapons[1].SetActive(true);
            akimbo = true;
        }
    }

    void Fire()
    {
        if (canFire == true && akimbo == true)
        {
            Instantiate(bullet, shotSpawns[0].position, shotSpawns[0].rotation);
            Instantiate(bullet, shotSpawns[1].position, shotSpawns[1].rotation);
            ammo--;
            UpdateAmmo();
        }
        else if (canFire == true)
        {
            Instantiate(bullet, shotSpawns[0].position, shotSpawns[0].rotation);
            ammo--;
            UpdateAmmo();
        }
        
    }

}
