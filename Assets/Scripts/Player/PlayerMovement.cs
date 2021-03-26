using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkedBehaviour {

    Rigidbody2D rb;
    
    float speed_mult = 5;
    float max_speed = 10;

    void Start() {
        if (IsLocalPlayer)
        {
            
            rb = GetComponent<Rigidbody2D>();
            MovePlayer();
        }
    }

    // Update is called once per frame
    void Update() {
        if (IsLocalPlayer)
        {
            MovePlayer();
        }
    }

    void MovePlayer() {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), 0f);
        dir = Vector2.ClampMagnitude(dir, 1f);
        rb.AddForce(dir * speed_mult);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, max_speed);
    }
}
