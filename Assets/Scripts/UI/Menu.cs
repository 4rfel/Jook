using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.SceneManagement;
using MLAPI.SceneManagement;

public class Menu : NetworkedBehaviour {

    public void Host() {
        NetworkingManager.Singleton.StartHost();
        //SceneManager.LoadScene("Game");
        NetworkSceneManager.SwitchScene("Game");
    }

    public void Join() {
        NetworkingManager.Singleton.StartClient();
        NetworkSceneManager.SwitchScene("Game");

        //SceneManager.LoadScene("Game");
    }
}
