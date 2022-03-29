using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CrachatSpawner : MonoBehaviour
{
    [FormerlySerializedAs("projectil")] public GameObject projectile;
    Vector3 playerPos;
    public float projectilHigh;
    public float timeToLaunchProjectil = .5f;
    float timeLastLaunch = 0f;

    private void Start()
    {
        // projectilHigh = Player.Instance.transform.position.y * 5f;
    }
    void Update()
    {
        Debug.Log("Time to launch projectile :" + (timeToLaunchProjectil / Blockchain.Instance.latestDifficulty) + " Difficulty: " + Blockchain.Instance.latestDifficulty);
        if (Time.time - timeLastLaunch > timeToLaunchProjectil / Blockchain.Instance.latestDifficulty)
        {
            timeLastLaunch = Time.time;
            playerPos = Player.Instance.transform.position;
            Instantiate(projectile, new Vector3(playerPos.x, projectilHigh, playerPos.z), Quaternion.identity);
        }
    }
}
