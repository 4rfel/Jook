using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using UnityEngine;

public class PlayerHp : NetworkedBehaviour {

	public NetworkedVarFloat hp = new NetworkedVarFloat(100f);

	GameObject[] spawnPoints;

	private void Start() {
		spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
	}

	public void TakeDmg(float dmg) {
		hp.Value -= dmg;
		if(hp.Value < 0) {
			hp.Value = 100f;
			InvokeClientRpcOnEveryone(Respawn);
		}
	}

	[ClientRPC]
	void Respawn() {
		GetComponent<Transform>().position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
	}
}
