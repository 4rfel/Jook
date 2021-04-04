using UnityEngine;
using MLAPI;

public class CamController : NetworkedBehaviour {

	[SerializeField] private GameObject PlayerCamObj;

	// Start is called before the first frame update
	void Start() {
		if (IsLocalPlayer) {
			PlayerCamObj.SetActive(true);
		}
	}
}
