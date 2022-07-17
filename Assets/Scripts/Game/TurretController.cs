using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject turret;
    [SerializeField] protected float turretSpeed = 20;

    public GameObject gun;
    [SerializeField] protected float gunSpeed = 10;
    [SerializeField] protected float gunMax = 0.1f;
    [SerializeField] protected float gunMin = -0.15f;

    public GameObject bulletPrefab;
    [SerializeField] protected float bulletForce = 2000;


    public void RotationTurret(float angle)
    {
        turret.transform.Rotate(Vector3.up, angle);
    }

    public void RotationTurret(Quaternion angle)
    {
        turret.transform.rotation = angle;
    }

    public void RotationTurretToward(Quaternion angle)
    {
        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, angle, turretSpeed * Time.deltaTime);
    }

    public void UpDownGun(float angle)
    {
        gun.transform.Rotate(Vector3.left, angle);
        Quaternion adjust = gun.transform.localRotation;
        if (gun.transform.localRotation.x > gunMax)
        {
            adjust.x = gunMax;
            gun.transform.localRotation = adjust;
        }
        else if (gun.transform.localRotation.x < gunMin)
        {
            adjust.x = gunMin;
            gun.transform.localRotation = adjust;
        }
    }

    public void UpDownGun(Quaternion angle)
    {
        gun.transform.rotation = angle;
        Quaternion adjust = gun.transform.localRotation;
        if (gun.transform.localRotation.x > gunMax)
        {
            adjust.x = gunMax;
            gun.transform.localRotation = adjust;
        }
        else if (gun.transform.localRotation.x < gunMin)
        {
            adjust.x = gunMin;
            gun.transform.localRotation = adjust;
        }
    }

    public void UpDownGunToward(Quaternion angle)
    {
        gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, angle, turretSpeed * Time.deltaTime);
    }
    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(gun.transform.forward * bulletForce, ForceMode.Impulse);
    }
}