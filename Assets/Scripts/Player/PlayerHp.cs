using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;

public class PlayerHp : MonoBehaviour
{
    public NetworkedVarFloat hp = new NetworkedVarFloat(100f);


    public void TakeDmg(float dmg) {
        hp.Value -= dmg;
        Debug.Log("hp" + hp.Value);
    }
}
