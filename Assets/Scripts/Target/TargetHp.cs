using MLAPI;
using UnityEngine;

public class TargetHp : NetworkedBehaviour {

	public void TakeDmg() {
		Destroy(gameObject);
		//InvokeClientRpcOnEveryone(Die);
	}

	void Die() {
	}
}
