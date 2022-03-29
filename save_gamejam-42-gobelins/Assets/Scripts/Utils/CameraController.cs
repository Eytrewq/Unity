using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    public Vector3 cameraOffset;
    public float smoothFactor = 0.1f;

    private void Start()
    {
        //cameraOffset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = player.transform.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 10);
    }
}
