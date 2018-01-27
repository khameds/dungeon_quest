using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class LevelLimitWarp : MonoBehaviour
{
    [SerializeField] private Transform linkedWarp;
    [SerializeField] private bool horizontal = false;
    [SerializeField] private bool atTop = false;
    [SerializeField] private bool atRight = false;
    [SerializeField] private float offset = 0.1f;

    bool teleport = false;
    Vector2 destination;
    
    Rigidbody2D rb;

    private void Start()
    {
      //  destination = linkedWarp.position - this.gameObject.transform.position;
      //  print("destination (" + this.gameObject.name + "):" + destination);
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        rb = col.attachedRigidbody;

        int direction = 1;
  
        if (rb != null)
        {
            if (horizontal)
            {
                //print("size (" + col.gameObject.name + ") = (" + col.gameObject.GetComponent<BoxCollider2D>().size.x +"," + col.gameObject.GetComponent<BoxCollider2D>().size.y +")");
                //print("size (" + this.gameObject.name + ") = " + this.gameObject.GetComponent<BoxCollider2D>().size.x + "," +this.gameObject.GetComponent<BoxCollider2D>().size.y +")");

                //x heigth
                offset = col.gameObject.GetComponent<BoxCollider2D>().size.y + col.gameObject.GetComponent<CircleCollider2D>().radius * 2 + this.gameObject.GetComponent<BoxCollider2D>().size.y;

                direction = (atTop) ? 1 : -1;
                destination = new Vector2(rb.transform.position.x, linkedWarp.position.y + (offset * direction));
            }
            else
            {
                //print("size (" + col.gameObject.name + ") = (" + col.gameObject.GetComponent<BoxCollider2D>().size.x + "," + col.gameObject.GetComponent<BoxCollider2D>().size.y + ")");
                //print("size (" + this.gameObject.name + ") = " + this.gameObject.GetComponent<BoxCollider2D>().size.x + "," + this.gameObject.GetComponent<BoxCollider2D>().size.y + ")");

                //y weigth
                offset = col.gameObject.GetComponent<BoxCollider2D>().size.y/2  + this.gameObject.GetComponent<BoxCollider2D>().size.y;
                direction = (atRight) ? 1 : -1;
                destination = new Vector2(linkedWarp.position.x + (offset * direction), rb.transform.position.y);
            }
        }
        else return;    
        teleport = true;
    }
    
    private void FixedUpdate()
    {
        if (teleport)
        {
            /*
            int direction;

            if (horizontal)
            {
                direction = (atTop) ? 1 : -1;
                destination.y = destination.y + (offset * direction); 
            }
            else
            {
                direction = (atRight) ? 1 : -1;
                destination.x = destination.x + (offset * direction);
            }

            print(rb.position + "->" + (destination + rb.position)+0.1);
            rb.MovePosition(destination + rb.position);
            */
            rb.MovePosition(destination);        
            teleport = false;
            
           
        }
    }
}
