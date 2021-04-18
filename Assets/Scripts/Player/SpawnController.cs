using UnityEngine;
using MLAPI;
using UnityEngine.SceneManagement;

public class SpawnController : NetworkedBehaviour {

    [SerializeField] GunController gunController;

    bool spawned = false;
    void Start() {
        if (IsLocalPlayer) {
            Spawn();
        }
    }

	private void Update() {
		if (IsLocalPlayer) {
            if((SceneManager.GetActiveScene().name == "Tutorial" && !spawned)) {
                Spawn();
                spawned = true;
			}
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                Spawn();
            }
		}
	}

    public void Spawn() {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GetComponent<Transform>().position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        gunController.quantBullets = gunController.magazineSize;
    }
}
