using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;

public class WinCondition : NetworkedBehaviour {

	[SerializeField] GameObject pauseCanvas;

	private void Update() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        if (targets.Length == 0)
            pauseCanvas.SetActive(true);
	}

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

    public void Quit() {
        StopGame();
        Application.Quit();
    }
}
