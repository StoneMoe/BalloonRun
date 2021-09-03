using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashControl : MonoBehaviour {
    void Start() {
#if UNITY_SERVER
        Global.RunAs = Global.GameMode.Server;
        SceneManager.LoadScene("Game");
#else

        StartCoroutine(SwtichToMainMenu());
#endif
    }

    IEnumerator SwtichToMainMenu() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }
}
