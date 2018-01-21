using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelLimitWarp))]
public class LevelLimitWarp : MonoBehaviour
{
    [SerializeField] private Transform linkedWarp;
    [SerializeField] private bool horizontal = false;
    


	// Use this for initialization
	void Start()
	{
        Debug.Log("this position : " + this.transform.position);
	}
	
	// Update is called once per frame
	void Update()
	{
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
        Debug.Log(col.gameObject.name + " collids at " + col.gameObject.transform.position);
        //TODO : warp to linkedWarp location, and shortly disable warping (Time.deltatime <3 )

        if(horizontal)
            col.GetComponent<Rigidbody2D>().MovePosition(new Vector2(-col.GetComponent<Rigidbody2D>().position.x, col.GetComponent<Rigidbody2D>().position.y));
        else
            col.GetComponent<Rigidbody2D>().MovePosition(new Vector2(col.GetComponent<Rigidbody2D>().position.x, -col.GetComponent<Rigidbody2D>().position.y));

        Debug.Log(col.gameObject.name + " warp to " + col.gameObject.transform.position + " - " + linkedWarp.position);
    }
}
