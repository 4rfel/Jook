using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class LightController : NetworkedBehaviour {

    [SerializeField] private GameObject spotlight;
    [SerializeField] private Material material_dark;
    [SerializeField] private Material material_light;
    [SerializeField] private Renderer rend;

    private bool isLightOn;
    private bool wasLightOn;


    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer)
        {
            isLightOn = false;
            wasLightOn = false;
        }
        ChangeLightState();
    }

    // Update is called once per frame
    void Update() {
        if (IsLocalPlayer)
        {
            if (Input.GetMouseButton(1))
                isLightOn = !isLightOn;
        }
        if (wasLightOn != isLightOn)
            ChangeLightState();
    }

    void ChangeLightState() {
        if (isLightOn)
        {
            rend.material = material_light;
            spotlight.SetActive(true);
            wasLightOn = true;
        } 
        else
        {
            rend.material = material_dark;
            spotlight.SetActive(false);
            wasLightOn = false;
        }
    }
}
