using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : NetworkBehaviour
{
    public Rigidbody2D rg;

    public override void OnStartServer()
    {
        base.OnStartServer();

        Invoke("DirectionBall", 2);
    }

    void DirectionBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1) {
            rg.AddForce(new Vector2(20, -15));
        } else {
            rg.AddForce(new Vector2(-20, -15));
        }
    }

    void ResetBall()
    {
        rg.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("DirectionBall", 1);
    }

    [ServerCallback]
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = rg.velocity.x;
            vel.y = (rg.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rg.velocity = vel;
        }
        if (coll.collider.CompareTag("Wall"))
        {
            GameManager.instance.Score(coll.transform.name);
            RestartGame();
        }
    }
}
