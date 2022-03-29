using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovementCircular : MonoBehaviour
{
    public Vector3 player;
    Rigidbody rb;
    Vector3 direction;
    public float rangeToDestroyObject = 200f; 
    public float speedToMoveAround;
    public float speed;
    public float randomDirectionRange = 3f;
    private bool contactTerritory = false;
    [SerializeField] private float timeAlive = 5f;
    void Start()
    {
        //player = GameObject.Find("Player").transform.position;
        player = Player.Instance.transform.position;        
        rb = GetComponent<Rigidbody>();
        direction = (player - transform.position);
        direction = new Vector3(Random.Range(direction.x - randomDirectionRange, direction.x + randomDirectionRange), 0, direction.z);
    }

    void Update()
    {
        transform.position += direction.normalized * Time.deltaTime * Mathf.Clamp(speed, speed - 2, speed + 2); ;
        transform.GetChild(0).RotateAround(transform.position, Vector3.up, speedToMoveAround * Time.deltaTime);
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
