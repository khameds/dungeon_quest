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
    [SerializeField] private float offset = 1.25f;

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.attachedRigidbody;
        int direction = 1;
        Vector2 v2;

        if (rb != null)
        {
            //Debug.Log(col.gameObject.name + " collids at " + col.gameObject.transform.position);
            //TODO : warp to linkedWarp location, and shortly disable warping (Time.deltatime <3 )    
            if (horizontal)
            {
                direction = (atTop) ? 1 : -1;
                v2 = new Vector2(rb.position.x, linkedWarp.transform.position.y + (offset * direction));
                rb.MovePosition(v2);
            }
            else
            {
                direction = (atRight) ? 1 : -1;
                v2 = new Vector2(linkedWarp.transform.position.x + (offset * direction), rb.position.y);
                rb.MovePosition(v2);
            }
            Debug.Log("Warp " + col.gameObject.name + " : " + col.gameObject.transform.position + " -> " + v2);
        }
        else
        {
            Debug.Log("[Warp] Can't get collider Rigidbody2D");
            return;
        }
    }
}
