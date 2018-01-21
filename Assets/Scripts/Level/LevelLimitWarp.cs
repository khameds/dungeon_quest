using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelLimitWarp))]
public class LevelLimitWarp : MonoBehaviour
{
    [SerializeField] private Transform linkedWarp;
    [SerializeField] private bool horizontal = false;
    [SerializeField] private bool atTop = false;
    [SerializeField] private bool atRight = false;
    [SerializeField] private float offset = 1f;

    // Use this for initialization
    void Start()
    {
        Debug.Log("this position : " + this.transform.position);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.attachedRigidbody;
        int direction = 1;

        if (rb != null)
        {
            Debug.Log(col.gameObject.name + " collids at " + col.gameObject.transform.position);
            //TODO : warp to linkedWarp location, and shortly disable warping (Time.deltatime <3 )    
            if (horizontal)
            {
                direction = (atTop) ? 1 : -1;
                rb.MovePosition(new Vector2(rb.position.x, linkedWarp.transform.position.y + (offset * direction)));
            }
            else
            {
                direction = (atRight) ? 1 : -1;
                rb.MovePosition(new Vector2(linkedWarp.transform.position.x + (offset * direction), rb.position.y));
            }
            Debug.Log(col.gameObject.name + " warp to " + col.gameObject.transform.position + " - " + linkedWarp.position);
        }
        else
        {
            Debug.Log("[Warp] Can't get collider Rigidbody2D");
            return;
        }

    }
}
