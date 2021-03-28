using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkedBehaviour {


    Rigidbody2D rb;
    
    float speed_mult = 10f;
    float max_speed = 10f;
    float jumpMult = 500;

    bool hasJumped = false;

    void Start() {
        if (IsLocalPlayer)
        {
            rb = GetComponent<Rigidbody2D>();
            MovePlayer();
        }
    }

    void Update() {
        if (IsLocalPlayer)
        {
            MovePlayer();
            if(Input.GetKeyDown("space"))
                Jump();
        }
    }

    void MovePlayer() {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), 0f);
        rb.AddForce(dir * speed_mult);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -max_speed, max_speed), rb.velocity.y);
    }

    void Jump() {
        if (!hasJumped)
        {
            rb.AddForce(Vector2.up * jumpMult);
            //hasJumped = true;
        }
    }

    void OnCollisionEnter(Collision col) {
        // Print how many points are colliding with this transform
        Debug.Log("Points colliding: " + col.contacts.Length);

        // Print the normal of the first point in the collision.
        Debug.Log("Normal of the first point: " + col.contacts[0].normal);

        foreach (var item in col.contacts) {
            if (item.normal.x != -1f) {
                hasJumped = false;
				break;
            }
            Debug.DrawRay(item.point, item.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
        }
    }
}
