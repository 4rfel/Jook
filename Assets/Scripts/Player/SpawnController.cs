using UnityEngine;
using MLAPI;

public class SpawnController : NetworkedBehaviour {
    void Start() {
        if (IsLocalPlayer) {
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

            GetComponent<Transform>().position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;   
        }
    }
}
