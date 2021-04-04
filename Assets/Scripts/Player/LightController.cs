using UnityEngine;
using MLAPI;

public class LightController : NetworkedBehaviour {

	[SerializeField] private GameObject spotlight;
	[SerializeField] private SpriteRenderer playerRend;
	[SerializeField] private SpriteRenderer gunRend;

	void Start() {
		spotlight.SetActive(false);
		if (IsLocalPlayer) {
			isLightOn = false;
			wasLightOn = false;
			playerRend.maskInteraction = SpriteMaskInteraction.None;
			gunRend.maskInteraction = SpriteMaskInteraction.None;
			spotlight.SetActive(true);
		}

	}
}
