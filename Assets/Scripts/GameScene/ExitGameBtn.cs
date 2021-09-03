using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameBtn : MonoBehaviour {
    NetworkManager gameManager;
    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NetworkManager>();
        gameObject.GetComponent<Button>().onClick.AddListener(OnExitGameClick);
    }
    public void OnExitGameClick() {
        if (Global.RunAs == Global.GameMode.Host) {
            gameManager.StopHost();
        } else if (Global.RunAs == Global.GameMode.Server) {
            gameManager.StopServer();
        } else if (Global.RunAs == Global.GameMode.Client) {
            gameManager.StopClient();
        } else {
            throw new UnityException("Invalid RunAs");
        }
    }
}
