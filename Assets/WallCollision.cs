using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision) {
        Debug.Log("Exit colliding with " + collision.gameObject.name);
        if (collision.gameObject.name == "Player") {
            Main.InWall = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Colliding with " + collision.gameObject.name);
        if (collision.gameObject.name == "Player") {
            Main.InWall = true;
        }
    }
}
