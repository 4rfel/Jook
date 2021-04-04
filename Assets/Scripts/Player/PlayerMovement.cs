using UnityEngine;
using MLAPI;

public class PlayerMovement : NetworkedBehaviour {

	[SerializeField] LayerMask Ground;

	Rigidbody2D rb;

	float max_speed = 10f;
	public float jumpMult = 10;

	void Start() {
		if (IsLocalPlayer) {
			rb = GetComponent<Rigidbody2D>();
			MovePlayer();
		}
	}

	void Update() {
		if (IsLocalPlayer) {
			MovePlayer();

			if (Input.GetKeyDown("space"))
				Jump();
		}
	}

	void MovePlayer() {
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * max_speed, rb.velocity.y);
	}

	void Jump() {
		if (CheckGround() || CheckWall()) {
			rb.velocity = new Vector2(rb.velocity.x, jumpMult);
		}
	}

	bool CheckGround() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, Ground);

		return (hit.collider != null);
	}

	bool CheckWall() {
		RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, Ground);
		RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, Ground);

		return (hitRight.collider != null || hitLeft.collider != null);
	}
}
