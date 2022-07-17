using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float outMap = 1000f;
    public GameObject terrainImpactEffect;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x > outMap 
            || transform.position.x < -outMap 
            || transform.position.z > outMap 
            || transform.position.z < -outMap 
            || transform.position.y > outMap 
            || transform.position.y < -outMap) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 position = transform.position;
        if (other.CompareTag("Terrain"))
        {
            Instantiate(terrainImpactEffect, transform.position, terrainImpactEffect.transform.rotation);
            Destroy(gameObject);
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            return;
        }
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.Hitting(position);
            return;
        }
    }
}
