using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkedBehaviour {

    [SerializeField] LayerMask Ground;

    Rigidbody2D rb;

    float max_speed = 10f;
    float jumpMult = 500;
    
    void Start() {
        if ( IsLocalPlayer ) {
            rb = GetComponent<Rigidbody2D>();
            MovePlayer();
        }
    }

    void Update() {
        if ( IsLocalPlayer ) {
            MovePlayer();

            if ( Input.GetKeyDown("space") )
                Jump();
        }
    }

    void MovePlayer() {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * max_speed, rb.velocity.y);
    }

    void Jump() {
		if ( CheckGround() ) {
			rb.AddForce(Vector2.up * jumpMult);
		}
	}

    bool CheckGround() {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, Ground);
        if ( hit.collider != null ) {
            return true;
		} else {
            return false;
		}
	}
}
