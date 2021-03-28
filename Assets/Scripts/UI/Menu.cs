using UnityEngine;
using MLAPI;

public class Menu : NetworkedBehaviour {

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject cameraMenu;

    public void Host() {
        NetworkingManager.Singleton.StartHost();
        menu.SetActive(false);
        cameraMenu.SetActive(false);

	}

	public void Join() {
        NetworkingManager.Singleton.StartClient();
        menu.SetActive(false);
        cameraMenu.SetActive(false);

    }
}
