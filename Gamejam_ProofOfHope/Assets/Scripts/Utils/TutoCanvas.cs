using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoCanvas : MonoBehaviour
{
    public Canvas canvasInterface;
    private float timeStamp = 0.0f;

    void Start()
    {
        timeStamp = Time.time + 5.0f;
    }

    void Update()
    {
        if (timeStamp <= Time.time)
        {
            canvasInterface.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
