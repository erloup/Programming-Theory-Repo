using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : TurretController
{
    private float defaultFow = 60;
    private float zoom;
    public float Zoom { get => zoom; }
    private float maxZoom = 3.5f;
    private float health = 100;
    public GameObject explosionDestroy;
    public GameObject explosionHit;
    private GameManager gameManager;
    private float cooldown = 2;
    private bool isCooldown;
    private float actualCooldown;
    public float Cooldown { get => cooldown; }
    public bool IsCooldown { get => isCooldown; }
    public float ActualCooldown { get => actualCooldown; }
    public float Health
    {
        get => health;
        set
        {
            health = value;
            if (health < 0) health = 0;
            if(health > 100) health = 100;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        zoom = 1;
        isCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown)
        {
            actualCooldown += Time.deltaTime;
            if(actualCooldown >= cooldown)
            {
                isCooldown = false;
                actualCooldown = 0;
            }
        }
        CommandTurret();
        CommandGun();
        if(!isCooldown) CommandFire();
        CommandZoom();
    }

    private void CommandTurret()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        this.RotationTurret(horizontalInput * turretSpeed * Time.deltaTime);
    }
    private void CommandGun()
    {
        float verticalInput = Input.GetAxis("Vertical");
        this.UpDownGun(verticalInput * gunSpeed * Time.deltaTime);
    }
    private void CommandFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.Shoot();
            isCooldown = true;
        }
    }
    private void CommandZoom()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        if (scrollData != 0)
        {
            zoom += scrollData;
            zoom = Mathf.Clamp(zoom, 1, maxZoom);
            Camera.main.fieldOfView = defaultFow / zoom;
        }
    }

    public void Hitting(Vector3 position)
    {
        Health -= Random.Range(1f, 10f);
        if(health == 0) Instantiate(explosionDestroy, transform.position, transform.rotation);
        else Instantiate(explosionHit, position, explosionHit.transform.rotation);
        if (health == 0) gameManager.GameOver();
    }
}
