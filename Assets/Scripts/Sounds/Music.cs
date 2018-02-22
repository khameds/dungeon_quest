using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour {

    Object[] myMusic;
    public Slider volume;
    public Toggle mute;

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
        GetComponent<AudioSource>().volume = 0.5F;
    }

    public void changeVolume()
    {
        if (!mute.isOn)
            GetComponent<AudioSource>().volume = volume.value;
    }

    public void muteUnMute()
    {
        if (mute.isOn)
        {
            GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            GetComponent<AudioSource>().volume = volume.value;
        }


    }


}
