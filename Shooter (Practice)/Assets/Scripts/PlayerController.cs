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
    public int clipSize;
    public int maxAmmo = 100;
    public int weaponNum;

    public float untouchable;
    public float reloadTime;

    //Weapons Properties
    public GameObject bullet;
    public GameObject[] weapons;
    public Transform[] shotSpawns;

    [HideInInspector]
    public bool canFire = true, canHit = true, canJump = true, akimbo = false;

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

    //Moving,Shooting, and Weapon Switching
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
        
        if (canJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpStrength);
            canJump = false;
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
        //If Ammo for new Clip doesn't divide nicely checks to see if remaining ammo is greater or less than clipSize
        if (maxAmmo % clipSize != 0)
        {
            if(maxAmmo >  clipSize)
            {
                ammo = clipSize;
                maxAmmo -= clipSize;
            }
            else
            {
                ammo = maxAmmo;
                maxAmmo = 0;
            }
        }
        //Reloads as normal
        else
        {
            ammo = clipSize;
            maxAmmo -= clipSize;
        }
        canFire = true;
        gc.eventText.text = "";
        UpdateAmmo();
    }

    //Keeps track of current weapon ammo
    public void UpdateAmmo()
    {
        gc.ammoText.text = "Ammo: " + ammo + "/" + maxAmmo;
        //If clip ammo and total ammo is 0 player can't fire until they find ammo
        if (ammo <= 0 && maxAmmo == 0)
        {
            canFire = false;
            return;
        }
        //Reloads as normals
        else if (ammo <= 0)
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

    //Switches Between Player Weapons and Toggles Akimbo when necessary
    void WeaponSwitch()
    {
        switch (weaponNum)
        {
            case 0:
                weapons[weapons.Length - 1].SetActive(false);
                weapons[weaponNum].SetActive(true);
                weaponNum++;
                break;
            case 1:
                weapons[weaponNum].SetActive(true);
                akimbo = true;
                weaponNum++;
                break;
            case 2:
                weapons[weaponNum - 1].SetActive(false);
                weapons[weaponNum - 2].SetActive(false);
                weapons[weaponNum].SetActive(true);
                weaponNum = 0;
                akimbo = false;
                break;
            default:
                break;
        }
    }

    //Fires gun and subtracts from clip ammo
    void Fire()
    {
        if (canFire == true && akimbo == true)
        {
            Instantiate(bullet, shotSpawns[0].position, shotSpawns[0].rotation);
            Instantiate(bullet, shotSpawns[1].position, shotSpawns[1].rotation);
            ammo--;
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
