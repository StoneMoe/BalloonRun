using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuBtn : MonoBehaviour
{
    public GameObject playerNameInputObj;
    public GameObject serverAddrInputObj;

    void Start() {
        if (!serverAddrInputObj) {
            Debug.LogError("Invalid serverAddrInputObj");
        }
    }
    public void OnCreateRoomBtnClick() {
        Global.RunAs = Global.GameMode.Host;
        GameObject.FindGameObjectWithTag("MainCamera").transform.DOMoveY(-45, .5f);
    }
    public void OnJoinRoomBtnClick() {
        Global.RunAs = Global.GameMode.Client;
        GameObject.FindGameObjectWithTag("MainCamera").transform.DOMoveY(-45, .5f);
    }
    public void OnConnectHostBtnClick() {
        Global.PlayerName = playerNameInputObj.GetComponent<Text>().text;
        Global.ServerAddr = serverAddrInputObj.GetComponent<Text>().text;
        SceneManager.LoadScene("Game");
    }
    public void OnSettingBtnClick() {
        GameObject.FindGameObjectWithTag("MainCamera").transform.DOMoveX(80, .5f);
    }
    public void OnExitBtnClick() {
        Application.Quit();
    }

    public void OnBackBtnClick() {
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.DOMove(new Vector3(0,0, camera.transform.position.z), .5f);
    }
}
