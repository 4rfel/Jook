using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class LightController : NetworkedBehaviour {

    [SerializeField] private GameObject spotlight;
    [SerializeField] private SpriteRenderer playerRend;
    [SerializeField] private SpriteRenderer gunRend;

    private bool isLightOn;
    private bool wasLightOn;

    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer)
        {
            isLightOn = false;
            wasLightOn = false;
            playerRend.maskInteraction = SpriteMaskInteraction.None;
            gunRend.maskInteraction = SpriteMaskInteraction.None;
        }
        ChangeLightState();
    }

    // Update is called once per frame
    void Update() {
        if (IsLocalPlayer) {
			isLightOn = Input.GetMouseButton(1);
        }
        if (wasLightOn != isLightOn)
            ChangeLightState();
    }

    void ChangeLightState() {
        if (isLightOn) {
			spotlight.SetActive(true);
            wasLightOn = true;
		} else {
            spotlight.SetActive(false);
            wasLightOn = false;
        }
    }
}
