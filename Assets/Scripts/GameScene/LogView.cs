using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogView : MonoBehaviour {
    bool show;
    string outputBuf;
    static Queue logQueue = new Queue();

    private void Start() {
        if (Application.isEditor || Debug.isDebugBuild) {
            show = true;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            show = !show;
        }
    }

    void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable() {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type) {
        if (logQueue.Count > 20) {
            logQueue.Dequeue();
        }
        logQueue.Enqueue("[" + type + "] : " + logString);
        if (type == LogType.Exception) {
            logQueue.Enqueue(stackTrace);
        }
        outputBuf = string.Empty;
        foreach (string item in logQueue) {
            outputBuf += item + "\n";
        }
    }

    void OnGUI() {
        if (show) {
            var rect = new Rect(Global.leftTop, new Vector2(Screen.width, Screen.height / 2));
            GUI.Box(rect, Texture2D.blackTexture);
            GUI.Label(rect, outputBuf);
            if (NetworkClient.active) {
                rect = new Rect(Global.rightBottom - new Vector2(100, 20), new Vector2(100, 20));
                GUI.Box(rect, Texture2D.blackTexture);
                GUI.Label(rect, $"Ping: {Math.Round(NetworkTime.rtt * 1000)}ms");
            }

        }
    }
}