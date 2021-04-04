using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.Messaging;

public class GunController : NetworkedBehaviour {

	[SerializeField] private Transform gunTransform;
	[SerializeField] private ParticleSystem bulletParticleSystem;
	[SerializeField] private Camera PlayerCam;


	NetworkedVarBool shooting = new NetworkedVarBool(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly }, false);

	float fireRate = 10f;
	float shootTimer = 0f;

	float dmg = 5;

	private ParticleSystem.EmissionModule em;

	private void Start() {
		em = bulletParticleSystem.emission;
		bulletParticleSystem.Play();
	}
	void Update() {
		if (IsLocalPlayer) {
			Rotate();

			TestShoot();
		}
		em.rateOverTime = shooting.Value ? 10f : 0f;

	}

	void Rotate() {
		Vector3 diff = PlayerCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		gunTransform.rotation = Quaternion.Euler(0f, 0f, rot_z);
	}

	void TestShoot() {
		shooting.Value = Input.GetMouseButton(0);
		shootTimer += Time.deltaTime;

		if (shooting.Value && shootTimer >= 1f / fireRate) {
			shootTimer = 0f;
			InvokeServerRpc(Shoot);
		}
	}

	[ServerRPC]
	void Shoot() {
		RaycastHit2D hit = Physics2D.Raycast(bulletParticleSystem.transform.position, gunTransform.right, 25f);
		if (hit.collider != null) {

			var player = hit.collider.GetComponent<PlayerHp>();
			if (player != null) {
				player.TakeDmg(dmg);
			}
		}
	}
}
