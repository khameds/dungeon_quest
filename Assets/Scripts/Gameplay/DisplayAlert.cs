using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAlert : MonoBehaviour
{
    public static DisplayAlert instance;
    public static Text manche;

    public void Start()
    {
        //Binding the text in the scene
        manche = GameObject.Find("TextAlert").GetComponent<Text>();
    }

    void Awake()
    {
        instance = this;
    }

    public static void Print(string text)
    {   
        manche.text = text;
        instance.StartCoroutine("LimitedDisplay");
    }
        
    IEnumerator LimitedDisplay()
    {
        yield return new WaitForSeconds(2);

        manche.text = "";
    }
}
