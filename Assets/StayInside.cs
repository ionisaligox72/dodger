using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour {
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.

    void Start() {
        screenBounds = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var playerSize = transform.GetComponent<SpriteRenderer>().bounds.size;
        objectWidth = playerSize.x / 2;
        objectHeight = playerSize.y / 2;
        rb2d = GetComponent<Rigidbody2D> ();
    }

    void LateUpdate() {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        /*
        Debug.Log("Screen: " + Screen.width + "," + Screen.height);
        Debug.Log("position: " + transform.position + " clamped: " + viewPos + " bounds: " + screenBounds);
        */
        transform.position = viewPos;
        rb2d.velocity = Vector3.zero;
    }
}
