using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudMovementStraight : MonoBehaviour
{
    public Vector3 player;
    Rigidbody rb;
    Vector3 direction;
    public float speed;
    public float randomDirectionRange = 3f;
    public float rangeToDestroyObject = 200f;
    private bool contactTerritory = false;
    [SerializeField] private float timeAlive = 5f;
    void Start()
    {
        //  player = GameObject.Find("Player").transform.position;
        player = Player.Instance.transform.position;        
        rb = GetComponent<Rigidbody>();
        direction = (player - transform.position);
        direction = new Vector3(Random.Range(direction.x - randomDirectionRange, direction.x + randomDirectionRange), 0, direction.z);
    }
    void Update()
    {
        transform.position += direction.normalized * Time.deltaTime * Random.Range(speed - 2f, speed + 2f);
        timeAlive -= Time.deltaTime;
        if (timeAlive < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Territory"))
        {
            if (!contactTerritory)
            {
                contactTerritory = true;
                speed /= 2f;
            }
            direction = transform.position - other.transform.position;
        }
    }
}
