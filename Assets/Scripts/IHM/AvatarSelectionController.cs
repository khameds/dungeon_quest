using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarSelectionController : MonoBehaviour {

    public GameObject avatarList1;
    public GameObject avatarList2;
    public GameObject avatarList3;
    public GameObject avatarList4;
    public GameObject hide1;
    public GameObject hide2;
    public GameObject hide3;

    private int indexAvatar1;
    private int indexAvatar2;
    private int indexAvatar3;
    private int indexAvatar4;
    private GameObject[] avatars1;
    private GameObject[] avatars2;
    private GameObject[] avatars3;
    private GameObject[] avatars4;

    public Button start;

    // Use this for initialization
    void Start ()
    {
        avatars1 = new GameObject[avatarList1.transform.childCount];
        avatars2 = new GameObject[avatarList2.transform.childCount];
        avatars3 = new GameObject[avatarList3.transform.childCount];
        avatars4 = new GameObject[avatarList4.transform.childCount];

        //Fill the array
        for (int i = 0; i < avatarList1.transform.childCount; i++)
        {
            avatars1[i] = avatarList1.transform.GetChild(i).gameObject;
            avatars2[i] = avatarList2.transform.GetChild(i).gameObject;
            avatars3[i] = avatarList3.transform.GetChild(i).gameObject;
            avatars4[i] = avatarList4.transform.GetChild(i).gameObject;
        }


        //Toggle off the renderer
        foreach (GameObject go in avatars1)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in avatars2)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in avatars3)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in avatars4)
        {
            go.SetActive(false);
        }

        //Toggle the first index
        if (avatars1[0])
            avatars1[0].SetActive(true);
        if (avatars2[0])
            avatars2[0].SetActive(true);
        if (avatars3[0])
            avatars3[0].SetActive(true);
        if (avatars4[0])
            avatars4[0].SetActive(true);


    }



    // Update is called once per frame
    void Update ()
    {
        //GamepadManagement.getStateByUserNumber(2).DPad.Left == XInputDotNetPure.ButtonState.Pressed

        if (GamepadManagement.GamepadConnectedNumber() + 1 == 1)
        {
            hide1.SetActive(true);
            hide2.SetActive(true);
            hide3.SetActive(true);
        }
        if (GamepadManagement.GamepadConnectedNumber() + 1 == 2)
        {
            hide1.SetActive(false);
            hide2.SetActive(true);
            hide3.SetActive(true);
        }
        if (GamepadManagement.GamepadConnectedNumber() + 1 == 3)
        {
            hide1.SetActive(false);
            hide2.SetActive(false);
            hide3.SetActive(true);
        }
        if (GamepadManagement.GamepadConnectedNumber() + 1 == 4)
        {
            hide1.SetActive(false);
            hide2.SetActive(false);
            hide3.SetActive(false);
        }

        if (GamepadManagement.GamepadConnectedNumber()+1 == 4)
            player4AvatarSelectionListener();
        if (GamepadManagement.GamepadConnectedNumber()+1 >= 3)
            player3AvatarSelectionListener();
        if (GamepadManagement.GamepadConnectedNumber()+1 >= 2)
            player2AvatarSelectionListener();
        if (GamepadManagement.GamepadConnectedNumber()+1 >= 1)
            player1AvatarSelectionListener();

        if (GamepadManagement.GamepadConnectedNumber() + 1 < 2)
            start.interactable = false;
        else
            start.interactable = true;
    }

    private void player1AvatarSelectionListener()
    {
        if (Input.GetKeyDown("left"))
        {
            //Toggle off the current avatar
            avatars1[indexAvatar1].SetActive(false);

            indexAvatar1--;
            if (indexAvatar1 < 0)
                indexAvatar1 = avatars1.Length - 1;

            //Toggle on the new avatar
            avatars1[indexAvatar1].SetActive(true);
        }
        if (Input.GetKeyDown("right"))
        {
            //Toggle off the current avatar
            avatars1[indexAvatar1].SetActive(false);

            indexAvatar1++;
            if (indexAvatar1 >= avatars1.Length)
                indexAvatar1 = 0;

            //Toggle on the new avatar
            avatars1[indexAvatar1].SetActive(true);
        }
        
    }
    private void player2AvatarSelectionListener()
    {
        if (GamepadManagement.getStateByUserNumber(1).DPad.Left == ButtonState.Released && GamepadManagement.getPrevStateByUserNumber(1).DPad.Left == ButtonState.Pressed)
        {
            //Toggle off the current avatar
            avatars2[indexAvatar2].SetActive(false);

            indexAvatar2--;
            if (indexAvatar2 < 0)
                indexAvatar2 = avatars2.Length - 1;

            //Toggle on the new avatar
            avatars2[indexAvatar2].SetActive(true);
        }
        if (GamepadManagement.getStateByUserNumber(1).DPad.Right == ButtonState.Released && GamepadManagement.getPrevStateByUserNumber(1).DPad.Right == ButtonState.Pressed)
        {
            //Toggle off the current avatar
            avatars2[indexAvatar2].SetActive(false);

            indexAvatar2++;
            if (indexAvatar2 >= avatars2.Length)
                indexAvatar2 = 0;

            //Toggle on the new avatar
            avatars2[indexAvatar2].SetActive(true);
        }
    }
    private void player3AvatarSelectionListener()
    {
        if (GamepadManagement.getStateByUserNumber(2).DPad.Left == ButtonState.Released && GamepadManagement.getPrevStateByUserNumber(2).DPad.Left == ButtonState.Pressed)
        {
            //Toggle off the current avatar
            avatars3[indexAvatar3].SetActive(false);

            indexAvatar3--;
            if (indexAvatar3 < 0)
                indexAvatar3 = avatars3.Length - 1;

            //Toggle on the new avatar
            avatars3[indexAvatar3].SetActive(true);
        }
        if (GamepadManagement.getStateByUserNumber(2).DPad.Right == ButtonState.Released && GamepadManagement.getPrevStateByUserNumber(2).DPad.Right == ButtonState.Pressed)
        {
            //Toggle off the current avatar
            avatars3[indexAvatar3].SetActive(false);

            indexAvatar3++;
            if (indexAvatar3 >= avatars3.Length)
                indexAvatar3 = 0;

            //Toggle on the new avatar
            avatars3[indexAvatar3].SetActive(true);
        }
    }
    private void player4AvatarSelectionListener()
    {
        if (GamepadManagement.getStateByUserNumber(3).DPad.Left == ButtonState.Released && GamepadManagement.getPrevStateByUserNumber(3).DPad.Left == ButtonState.Pressed)
        {
            //Toggle off the current avatar
            avatars4[indexAvatar4].SetActive(false);

            indexAvatar4--;
            if (indexAvatar4 < 0)
                indexAvatar4 = avatars4.Length - 1;

            //Toggle on the new avatar
            avatars4[indexAvatar4].SetActive(true);
        }
        if (GamepadManagement.getStateByUserNumber(3).DPad.Left == ButtonState.Released && GamepadManagement.getPrevStateByUserNumber(3).DPad.Left == ButtonState.Pressed)
        {
            //Toggle off the current avatar
            avatars4[indexAvatar4].SetActive(false);

            indexAvatar4++;
            if (indexAvatar4 >= avatars4.Length)
                indexAvatar4 = 0;

            //Toggle on the new avatar
            avatars4[indexAvatar4].SetActive(true);
        }
    }

    public void StartGame()
    {
        int nb_players = GamepadManagement.GamepadConnectedNumber() +1 ;

        LevelParam.Set("color1", indexAvatar1.ToString());
        LevelParam.Set("color2", indexAvatar2.ToString());
        if (nb_players > 2)
            LevelParam.Set("color3", indexAvatar3.ToString());
        if (nb_players > 3)
            LevelParam.Set("color4", indexAvatar4.ToString());


        SceneManager.LoadScene("sandbox");
    }
}
