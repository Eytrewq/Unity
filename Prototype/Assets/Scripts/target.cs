using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    private GameManager gameManager;
    private Rigidbody rbTarget;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float yPosSpawn = -3;
    public int pointValue;
    void Start()
    {
        rbTarget = GetComponent<Rigidbody>();
        rbTarget.AddForce(RandomForce(), ForceMode.Impulse);
        rbTarget.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
            gameManager.UpdateLives(1);
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive && !gameManager.isGamePause)
        {
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }

    Vector3 RandomForce()
    {
        return (Vector3.up * Random.Range(minSpeed, maxSpeed));
    }
    float RandomTorque()
    {
        return (Random.Range(-maxTorque, maxTorque));
    }
    Vector3 RandomSpawnPos()
    {
        return (new Vector3(Random.Range(-xRange, xRange), yPosSpawn));
    }
}
