using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SideWalls : NetworkBehaviour
{
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Ball")
        {
            GameManager.instance.Score(transform.name);
            hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);
        }
    }
}
