using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using MLAPI;

public class PlayerPause : NetworkedBehaviour {
    [SerializeField] GameObject pauseCanvas;


    public bool paused = false;

    public void Pause(bool p) {
        pauseCanvas.SetActive(p);
        paused = p;
    }

    public void Pause() => Pause(true);

    public void Unpause() => Pause(false);
    public void StopGame() {
        if (IsHost)
            NetworkingManager.Singleton.StopHost();
        else
            NetworkingManager.Singleton.StopClient();
    }

    public void ToMenu() {
        StopGame();
        SceneManager.LoadScene("Game");
    }

    void Start() => Unpause();

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause(!paused);
        }
    }

    public void Quit() {
        StopGame();
        Application.Quit();
    }
}
