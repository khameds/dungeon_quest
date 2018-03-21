using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour {

    Object[] myMusic;
    public Slider volume;
    public Toggle mute;
    public static AudioSource currentMusic;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        myMusic = Resources.LoadAll("Music", typeof(AudioClip));
        GetComponent<AudioSource>().clip = myMusic[0] as AudioClip;
    }

    // Use this for initialization
    void Start ()
    {
        if (currentMusic == null)
        {
            currentMusic = GetComponent<AudioSource>();
            currentMusic.Play();
            currentMusic.volume = float.Parse(PlayerPrefs.GetString("volume", "0.5"));
        }
        volume.value = currentMusic.volume;
    }

    public void changeVolume()
    {
        if (!mute.isOn)
        {
            currentMusic.volume = volume.value;
            PlayerPrefs.SetString("volume", volume.value.ToString());
        }
    }

    public void muteUnMute()
    {
        if (mute.isOn)
        {
            currentMusic.volume = 0;
            PlayerPrefs.SetString("volume", "0");
        }
        else
        {
            currentMusic.volume = volume.value;
            PlayerPrefs.SetString("volume", volume.value.ToString());
        }
    }


}
