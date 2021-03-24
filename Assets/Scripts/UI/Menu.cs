using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Menu : NetworkedBehaviour {

    [SerializeField] private GameObject menu;

    public void Host() {
        NetworkingManager.Singleton.StartHost();
        menu.SetActive(false);
    }

    public void Join() {
        NetworkingManager.Singleton.StartClient();
        menu.SetActive(false);

    }
}
