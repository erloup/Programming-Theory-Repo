using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Enemy : TurretController
{
    public GameObject explosion;
    protected bool isDestroy;
    protected float disparition = 0.5f;
    protected float diffDisparition;
    protected GameObject playerTank;
    protected Quaternion focusRotationTurret;
    protected Quaternion focusRotationGun;
    [SerializeField] protected float precision;
    public float gunAdjust;
    protected float shootIntervaMin = 3;
    protected float shootIntervaMax = 10;
    protected GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerTank = GameObject.Find("Player").gameObject.transform.Find("Player Tank").gameObject;
        isDestroy = false;
        diffDisparition = 0;
        FocusPlayer();
        Invoke("Shoot", Random.Range(shootIntervaMin, shootIntervaMax));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDestroy)
        {
            diffDisparition += disparition * Time.deltaTime;
            transform.Translate(Vector3.down * disparition * Time.deltaTime);
        }
        if(diffDisparition > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            isDestroy = true;
            gameManager.NbEnemies--;
        }
    }
    // ABSTRACTION
    protected virtual void FocusPlayer()
    {
        Vector3 lookTurret = playerTank.transform.position - turret.transform.position;
        lookTurret.y = 0;
        focusRotationTurret = Quaternion.LookRotation(lookTurret, Vector3.up);
        RotationTurret(focusRotationTurret);

        Vector3 lookGun = playerTank.transform.position - turret.transform.position;
        focusRotationGun = Quaternion.LookRotation(lookGun, Vector3.up);
        UpDownGun(focusRotationGun);
    }

    // POLYMORPHISM
    public override void Shoot()
    {
        if (isDestroy) return;
        RotationTurret(Quaternion.Euler((focusRotationTurret.eulerAngles + GeneratePrecisionTurret())));
        UpDownGun(Quaternion.Euler((focusRotationGun.eulerAngles + GeneratePrecisionGun())));
        base.Shoot();
        Invoke("Shoot", Random.Range(shootIntervaMin, shootIntervaMax));
    }

    protected Vector3 GeneratePrecisionTurret()
    {
        return new Vector3(0, Random.Range(-precision, precision), 0);
    }
    protected Vector3 GeneratePrecisionGun()
    {
        return new Vector3(Random.Range(-precision, precision) + gunAdjust,0 ,0);
    }
}
