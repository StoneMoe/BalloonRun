using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject followTarget;
    float targetDistance;

    /// <summary>
    /// ÉèÖÃÒª¸ú×ÙµÄGameObject
    /// </summary>
    /// <param name="obj"></param>
    public void setLocalPlayer(GameObject obj) {
        this.followTarget = obj;
    }

    // Update is called once per frame
    void Update()
    {
        if (!followTarget) {
            return;
        }
        targetDistance = Vector2.Distance(
            new Vector2(followTarget.transform.position.x, 0), new Vector2(transform.position.x, 0));
        if (targetDistance > 5) {
            if (followTarget.transform.position.x < gameObject.transform.position.x) { // follow target is on the left
                transform.position = new Vector3(followTarget.transform.position.x + 5, transform.position.y, transform.position.z);
            } else {
                transform.position = new Vector3(followTarget.transform.position.x - 5, transform.position.y, transform.position.z);
            }
        }
    }

    void OnGUI() {
        if (Application.isEditor && followTarget) {
            GUI.Box(new Rect(0, Screen.height - 30, 300, 30), Texture2D.blackTexture);
            GUI.Label(new Rect(10, Screen.height - 30, 300, 30), string.Format("Camera distance: {0}", targetDistance));
        }
    }
}
