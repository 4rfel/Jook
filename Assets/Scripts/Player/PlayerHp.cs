using MLAPI;
using MLAPI.NetworkedVar;

public class PlayerHp : NetworkedBehaviour {
	public NetworkedVarFloat hp = new NetworkedVarFloat(100f);


	public void TakeDmg(float dmg) {
		hp.Value -= dmg;
	}
}
