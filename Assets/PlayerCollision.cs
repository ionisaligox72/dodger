using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameObject player;
    //private Color originalColour;
    //public Animator scaredAnimator;

    private void Start()
    {
        player = GameObject.Find("Player");
        //originalColour = player.GetComponent<Renderer>().material.color;
        //scaredAnimator = player.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Trigger enter " + other.gameObject.layer);
        var asteroidLayer = LayerMask.NameToLayer("Asteroid trigger");
        if (other.gameObject.layer == asteroidLayer) {
            Main.Collisions++;
            Main.IsCollision = true;
            //scaredAnimator.Play("Scared animation");
            //StartCoroutine("EnemyFlash");
        }
    }

    //public IEnumerator EnemyFlash() {
    //    Debug.Log("Flashing");
    //    material.color = Color.red;
    //    yield return new WaitForSeconds(0.1f);
    //    material.color = originalColour;
    //    StopCoroutine("EnemyFlash");
    //    Debug.Log("End flashing");
    //}

    //void OnCollisionEnter(Collision other)
    //{
    //    Debug.Log("Collision detected");
    //}
}
