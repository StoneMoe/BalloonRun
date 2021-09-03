using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : NetworkBehaviour
{
    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            NetworkServer.RemovePlayerForConnection(collision.gameObject.GetComponent<NetworkIdentity>().connectionToClient, true);
        }
    }
}
