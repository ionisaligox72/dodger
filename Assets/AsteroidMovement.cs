using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public Vector3 Direction;
    public float Speed;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(Direction * Speed * 100);
    }

    public void Accelerate(float factor) {
        rb2d.velocity *= factor;
    }

    public void Configure(Vector2 direction, float speed, float scale = 0.5f)
    {
        Direction = direction.normalized;
        Speed = speed;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
