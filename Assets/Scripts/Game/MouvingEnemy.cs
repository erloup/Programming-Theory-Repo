using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvingEnemy : Enemy
{
    public Vector3 position1;
    public Vector3 position2;
    private Vector3 goToPosition;
    private float rotateSpeed = 10;
    private float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerTank = GameObject.Find("Player").gameObject.transform.Find("Player Tank").gameObject;
        transform.position = position1;
        goToPosition = position2;
        isDestroy = false;
        diffDisparition = 0;
        base.FocusPlayer();
        Invoke("Shoot", Random.Range(shootIntervaMin, shootIntervaMax));
        //Invoke("Shoot", Random.Range(shootIntervaMin, shootIntervaMax));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDestroy)
        {
            diffDisparition += disparition * Time.deltaTime;
            transform.Translate(Vector3.down * disparition * Time.deltaTime);
        }
        if (diffDisparition > 5)
        {
            Destroy(gameObject);
        }
        if (!isDestroy)
        {
            Move();
            FocusPlayer();
        }
    }

    protected override void FocusPlayer()
    {
        Vector3 lookTurret = playerTank.transform.position - turret.transform.position;
        lookTurret.y = 0;
        focusRotationTurret = Quaternion.LookRotation(lookTurret, Vector3.up);
        RotationTurretToward(focusRotationTurret);

        Vector3 lookGun = playerTank.transform.position - turret.transform.position;
        focusRotationGun = Quaternion.LookRotation(lookGun, Vector3.up);
        UpDownGunToward(focusRotationGun);
    }

    public void Move()
    {
        if(this.transform.position == goToPosition)
        {
            if (goToPosition == position1) goToPosition = position2;
            else goToPosition = position1;
        }
        Quaternion orientation = Quaternion.LookRotation(goToPosition - transform.position);
        if(transform.rotation != orientation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, orientation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, goToPosition, moveSpeed * Time.deltaTime);
        }
    }
}
