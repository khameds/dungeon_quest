using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    Object[] myMusic;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        myMusic = Resources.LoadAll("Music", typeof(AudioClip));
        GetComponent<AudioSource>().clip = myMusic[0] as AudioClip;
    }

    // Use this for initialization
    void Start ()
    {
        GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
