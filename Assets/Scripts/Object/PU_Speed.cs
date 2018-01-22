using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Speed : MonoBehaviour {

    public float multiplier = 1.5f;
    public float duration = 4f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            StartCoroutine(Pickup(other));
        }
    }
    // Update is called once per frame

    IEnumerator Pickup(Collider2D player)
    {
        Debug.Log("Picked up");
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        gameObject.GetComponent<Collider2D>().enabled = false;

        player.GetComponent<PlayerController>().maxSpeed *= multiplier;

        yield return new WaitForSeconds(duration);


        player.GetComponent<PlayerController>().maxSpeed /= multiplier;


        Destroy(gameObject);
    }
}
