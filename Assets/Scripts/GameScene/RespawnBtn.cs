using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnBtn : MonoBehaviour {
    NetworkManager gameManager;
    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NetworkManager>();
        gameObject.GetComponent<Button>().onClick.AddListener(OnRespawnClick);
    }
    public void OnRespawnClick() {
        NetworkClient.AddPlayer();
    }
}
