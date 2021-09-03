using Mirror;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (gameObject.GetComponentInParent<Rigidbody2D>().velocity.y < 0 && collision.gameObject.tag == "Player") { // attack only valid when player falling
            gameObject.GetComponentInParent<Player>().CmdMakeDamage(collision.gameObject, 1);
        }
    }
}
