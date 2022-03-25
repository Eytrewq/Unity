using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public float speed = 600;
    public Rigidbody2D rg;

    void FixedUpdate()
    {
        if (isLocalPlayer)
            rg.velocity = new Vector2(0, Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;
    }
}
