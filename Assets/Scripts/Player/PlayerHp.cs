using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using UnityEngine;

public class PlayerHp : NetworkedBehaviour {

    NetworkedVarFloat hp = new NetworkedVarFloat(100f);

    [SerializeField] SpawnController spawnController;
    [SerializeField] GameObject matObj;

    Material mat;

    private void Start() {
        if (IsLocalPlayer) {
            matObj.SetActive(true);
            mat = matObj.GetComponent<SpriteRenderer>().material;
        }
    }

    public void TakeDmg(float dmg) {
        hp.Value -= dmg;
        mat.SetFloat("_Hp", hp.Value / 100f);
        if (hp.Value < 0) {
            hp.Value = 100f;
            mat.SetFloat("_Hp", 1f);
            InvokeClientRpcOnEveryone(Respawn);
        }
    }

    [ClientRPC]
    void Respawn() {
        spawnController.Spawn();
    }
}
