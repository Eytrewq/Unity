using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class Swipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;

    private bool isSwipe = false;

    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwipe = true;
                UpdateComponent();
            }
            if (Input.GetMouseButtonUp(0))
            {
                isSwipe = false;
                UpdateComponent();
            }
            if (isSwipe)
                UpdateMousePos();
        }
    }

    void UpdateMousePos()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        transform.position = mousePos;
    }

    void UpdateComponent()
    {
        trail.enabled = isSwipe;
        col.enabled = isSwipe;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<target>())
            collision.gameObject.GetComponent<target>().DestroyTarget();
    }
}
