using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.SceneManagement;

public class Menu : NetworkedBehaviour {

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject camera;


    public void Host() {
        NetworkingManager.Singleton.StartHost();
        menu.SetActive(false);
        camera.SetActive(false);

    }

    public void Join() {
        NetworkingManager.Singleton.StartClient();
        menu.SetActive(false);
        camera.SetActive(false);

    }
}
