using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour {
    [Tooltip("用于操控变向时，Flip 视觉元素")]
    public GameObject spriteObj;
    [Tooltip("用于用户名展示的Canvas Text")]
    public GameObject nickTextObj;
    [Tooltip("基础血量")]
    public int baseHealth = 3;
    [Tooltip("默认水平速度")]
    public float hSpeed = 10;
    [Tooltip("默认垂直速度")]
    public float vSpeed = 20;
    [Tooltip("最大空速")]
    public float maxAirSpeed = 20;
    [Tooltip("玩家间反弹力度")]
    public float playerBounceForce = 12;

    [SyncVar(hook = "OnNicknameSynced")]
    public string nickname = string.Empty;
    [SyncVar]
    public int health = 3;

    private GameObject FlyHeightLimitRef;
    private Rigidbody2D body;
    private float baseScaleX;
    void Start() {
        if (!spriteObj) {
            throw new UnityException(string.Format("Invalid spriteObj on game object \"{0}\"", gameObject.name));
        }
        if (!nickTextObj) {
            throw new UnityException(string.Format("Invalid nickTextObj on game object \"{0}\"", gameObject.name));
        }
        FlyHeightLimitRef = GameObject.FindGameObjectWithTag("MaxFlyHeight");
        body = gameObject.GetComponent<Rigidbody2D>();
        baseScaleX = spriteObj.transform.localScale.x;
        Debug.Log(string.Format("Player({0}) spawned", netId));
    }

    public override void OnStartLocalPlayer() {
        CmdSetNickname(Global.PlayerName);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>().setLocalPlayer(gameObject);
    }

    void FixedUpdate() {
        if (!isLocalPlayer) {
            return;
        }
        // Air speed limit
        if (body.velocity.magnitude > maxAirSpeed) {
            body.velocity = body.velocity.normalized * maxAirSpeed;
        }
        // Movement
        if (Input.GetKey(KeyCode.Space) && gameObject.transform.position.y <= FlyHeightLimitRef.transform.position.y) {
            body.AddForce(new Vector2(0, vSpeed));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            body.AddForce(new Vector2(-hSpeed, 0));
            changeScaleX(baseScaleX);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            body.AddForce(new Vector2(hSpeed, 0));
            changeScaleX(-baseScaleX);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        var playerObj = collision.gameObject.GetComponent<Player>()?.gameObject;
        if (playerObj) {
            // only apply bounce force for player collisions
            var contactNormal = collision.GetContact(0).normal;
            var force = contactNormal.normalized * playerBounceForce;
            body.velocity = force;
        }
    }

    #region SyncVar hooks
    void OnNicknameSynced(string oldValue, string newValue) {
        nickTextObj.GetComponent<Text>().text = newValue;
        Debug.Log(string.Format("Player({0}) set name to \"{1}\"", netId, newValue));
    }
    #endregion

    #region Run on server
    [Command]
    public void CmdSetNickname(string name) {
        nickname = name;
    }

    [Command]
    public void CmdMakeDamage(GameObject attackTarget, int amount) {
        // TODO: anti-cheat detection here
        Player opponentPlayer = attackTarget.GetComponent<Player>();

        opponentPlayer.health -= amount;
        if (opponentPlayer.health == 0) {
            NetworkIdentity opponentIdentity = attackTarget.GetComponent<NetworkIdentity>();
            NetworkServer.RemovePlayerForConnection(opponentIdentity.connectionToClient, true);
        } else {
            opponentPlayer.RpcGotDamage(amount);
        }
    }
    #endregion

    #region Run on client
    [ClientRpc]
    public void RpcGotDamage(int amount) {
        print(string.Format("\"{0}\" is getting damage", nickname));
        HideOneBalloon();
    }
    #endregion

    #region sprite helper
    void changeScaleX(float x) {
        Vector3 cur = spriteObj.transform.localScale;
        cur.x = x;
        spriteObj.transform.localScale = cur;
    }
    public void HideOneBalloon() {
        foreach (var comp in spriteObj.GetComponentsInChildren<SpriteRenderer>()) {
            if (comp.gameObject.tag == "Balloon") {
                comp.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void ShowAllBalloon() {
        foreach (var comp in spriteObj.GetComponentsInChildren<SpriteRenderer>()) {
            if (comp.gameObject.tag == "Balloon") {
                comp.gameObject.SetActive(true);
            }
        }
    }
    #endregion
}