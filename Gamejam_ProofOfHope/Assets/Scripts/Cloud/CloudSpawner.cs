using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloud;
    public float timeToLaunchCloud = .5f;
    float timeLastLaunch = 0f;
    Rigidbody rb;
    Vector3 direction;
    Vector3 playerPos;
    public Pillar attackPillar;
    [SerializeField] private float speed = 2f;

    private float difficulty = 1f;

    private void Start()
    {
        playerPos = Player.Instance.transform.position;
        
        StartCoroutine(FetchDifficultyCoroutine());
    }

    private void OnDifficultyFetched(float difficulty) {
        this.difficulty = difficulty;
        Debug.Log("Difficulty is fetched in CloudSpawner :" + this.difficulty);
    }

    private IEnumerator FetchDifficultyCoroutine() {
        for (;;) {
            Blockchain.Instance.FetchDifficulty(this.OnDifficultyFetched);
            yield return new WaitForSeconds(15f);
        }
    }
    void Update()
    {
        timeLastLaunch += Time.deltaTime * difficulty;
        Debug.Log("timeLastLaunch: " + timeLastLaunch);
        if (timeLastLaunch > timeToLaunchCloud)
        {
            timeLastLaunch = 0;
            Instantiate(cloud, transform.position + (playerPos-transform.position) * 0.1f, Quaternion.identity);
        }

        if (attackPillar)
        {
            transform.position += Vector3.Scale(new Vector3(1,0,1), attackPillar.transform.position - transform.position) * Time.deltaTime * speed;
        }
    }
}
