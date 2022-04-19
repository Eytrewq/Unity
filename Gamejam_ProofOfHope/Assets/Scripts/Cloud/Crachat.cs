using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crachat : MonoBehaviour
{
    public Vector3 player;
    Vector3 direction;
    public float speedProjectil;
    public float randomDirectionRange = 3f;
    public GameObject groundMarker;
    GameObject currentInstantiation;


    void Start()
    {
        player = Player.Instance.transform.position;
        if (groundMarker != null)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(new Ray(transform.position, Vector3.down), 100f);
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.tag == "Ground")
                    {
                        Vector3 fallPosition = hits[i].point;
                        currentInstantiation = Instantiate(groundMarker, fallPosition - 0.05f * Vector3.down, Quaternion.identity);
                        break;
                    }
                }
            }
        }
               
        Debug.DrawRay(transform.position, Vector3.down * 100f, Color.red, 5f);
    }

    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * Mathf.Clamp(speedProjectil, speedProjectil - 2, speedProjectil + 2);
        if (transform.position.y < -100f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Territory.Instance.ShrinkTerritory();
        }
    }
}