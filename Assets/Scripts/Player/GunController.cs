using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.Messaging;
using System.Collections;
using UnityEngine.UI;


public class GunController : NetworkedBehaviour {

	[SerializeField] private Transform gunTransform;
	[SerializeField] private ParticleSystem bulletParticleSystem;
	[SerializeField] private Camera PlayerCam;
	[SerializeField] private GameObject bulletsObj;
	[SerializeField] private Text bulletsTxt;

	NetworkedVarBool shooting = new NetworkedVarBool(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly }, false);

	float fireRate = 10f;
	float shootTimer = 0f;

	float dmg = 5;

	private ParticleSystem.EmissionModule em;

	public int magazineSize = 20;
	public int quantBullets = 20;

	bool reloading = false;

	private void Start() {
		em = bulletParticleSystem.emission;
		bulletParticleSystem.Play();

		if (IsLocalPlayer) {
			bulletsObj.SetActive(true);
		}
	}

	void Update() {
		if (IsLocalPlayer) {
			Rotate();

			TestShoot();

			if ((Input.GetKeyDown(KeyCode.R) || quantBullets <= 0 ) && !reloading) {
				IEnumerator coroutine = Reload();
				StartCoroutine(coroutine);
			}
		}
		if(quantBullets > 0 && !reloading) em.rateOverTime = shooting.Value ? 10f : 0f;
		else em.rateOverTime = 0f;

		bulletsTxt.text = "balas: " + quantBullets;
	}

	void Rotate() {
		Vector3 diff = PlayerCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		gunTransform.rotation = Quaternion.Euler(0f, 0f, rot_z);
	}

	[ClientRPC]
	IEnumerator Reload() {
		reloading = true;
		yield return new WaitForSeconds(1f);
		quantBullets = magazineSize;
		reloading = false;
	}

	void TestShoot() {
		shooting.Value = Input.GetMouseButton(0);
		shootTimer += Time.deltaTime;

		if (shooting.Value && shootTimer >= 1f / fireRate && quantBullets > 0 && !reloading) {
			shootTimer = 0f;
			quantBullets--;
			InvokeServerRpc(Shoot);
		}
	}

	[ServerRPC]
	void Shoot() {
		RaycastHit2D hit = Physics2D.Raycast(bulletParticleSystem.transform.position, gunTransform.right, 17f);
		Debug.DrawLine(gunTransform.right, gunTransform.right * 20f);
		if (hit.collider != null) {

			var player = hit.collider.GetComponent<PlayerHp>();
			if (player != null) {
				player.TakeDmg(dmg);
			}
		}
	}

	//private void OnDrawGizmos() {
	//	Gizmos.DrawLine(bulletParticleSystem.transform.position, gunTransform.right* 17f);
	//}
}
