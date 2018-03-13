using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Invisible : MonoBehaviour {

    
    public float duration = 4f;
    private bool change;
    private bool picked_up;
    Collider2D player_collided;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            StartCoroutine(Pickup(other));
            picked_up = true;
        }
    }
    // Update is called once per frame

    private void Start()
    {
        change = false;
        picked_up = false;
        player_collided = null;
    }

    private void Update()
    {
        if (!picked_up)
        {
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            if (tmp.a <= 0)
                change = true;
            if (tmp.a >= 1)
                change = false;

            if (change)
                tmp.a += 0.02f;
            else
                tmp.a -= 0.02f;

            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
        if(player_collided != null)
        {
            Color tmp_player = player_collided.GetComponent<SpriteRenderer>().color;
            tmp_player.a = 0.1f;
            player_collided.GetComponent<SpriteRenderer>().color = tmp_player;
        }
    }


    IEnumerator Pickup(Collider2D player)
    {
        Debug.Log("Picked up");
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
        gameObject.GetComponent<Collider2D>().enabled = false;

        player_collided = player;

        yield return new WaitForSeconds(4);

        player_collided = null;


        Destroy(gameObject);
    }
}
