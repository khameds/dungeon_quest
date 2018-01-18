using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollider : MonoBehaviour {

    public float multiplier = 2f;
    public float duration = 4f;
    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            
            StartCoroutine(Pickup(other));
        }
    }
	// Update is called once per frame

    IEnumerator Pickup (Collider2D player)
    {
        Debug.Log("Picked up");
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        gameObject.GetComponent<Collider2D>().enabled = false;

        Debug.Log(player.GetComponent<PlayerController>().maxSpeed);
        Debug.Log(multiplier);
        player.GetComponent<PlayerController>().maxSpeed *= multiplier;

        yield return new WaitForSeconds(duration);


        player.GetComponent<PlayerController>().maxSpeed /= multiplier;

        Destroy(gameObject);
    }




}
