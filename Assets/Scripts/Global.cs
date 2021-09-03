using UnityEngine;

public static class Global {
    public static Vector2 leftTop = new Vector2(0, 0);
    public static Vector2 rightTop = new Vector2(Screen.width, 0);
    public static Vector2 leftBottom = new Vector2(0, Screen.height);
    public static Vector2 rightBottom = new Vector2(Screen.width, Screen.height);
    public enum GameMode {
        Client, // Default
        Host,  // Server + Client
        Server,  // Headless server
    }
    public static GameMode RunAs = GameMode.Client;
    private static string serverAddr = string.Empty;
    public static string ServerAddr {
        get {
            if (serverAddr.Length != 0) {
                return serverAddr;
            } else {
                return "public.hestia.moe";
            }
        }
        set {
            var v = value.Trim();
            if (v != null && v.Length != 0) {
                serverAddr = v;
            } else {
                serverAddr = "public.hestia.moe";
            }
        }
    }
    private static string playerName = string.Empty;
    public static string PlayerName {
        get {
            if (playerName.Length != 0) {
                return playerName;
            } else {
                return "<Î´Ö¸¶¨>";
            }
        }
        set {
            var v = value.Trim();
            if (v.Length != 0) {
                playerName = v;
            }
        }
    }
}