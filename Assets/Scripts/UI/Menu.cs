using UnityEngine;
using MLAPI;
using MLAPI.Transports.UNET;
using UnityEngine.UI;

public class Menu : NetworkedBehaviour {

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject cameraMenu;
    [SerializeField] private InputField ipInputField;


    public void Host() {
        NetworkingManager.Singleton.StartHost();
        menu.SetActive(false);
        cameraMenu.SetActive(false);

	}

	public void Join() {
        if (ipInputField.text.Length > 0)
            NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = ipInputField.text;
        else
            NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = "127.0.0.1";

        NetworkingManager.Singleton.StartClient();
        menu.SetActive(false);
        cameraMenu.SetActive(false);

    }
}
