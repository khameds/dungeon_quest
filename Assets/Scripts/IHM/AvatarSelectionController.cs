using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelectionController : MonoBehaviour {

    public GameObject avatarList1;
    public GameObject avatarList2;
    public GameObject avatarList3;
    public GameObject avatarList4;
    private int indexAvatar1;
    private int indexAvatar2;
    private int indexAvatar3;
    private int indexAvatar4;
    private GameObject[] avatars1;
    private GameObject[] avatars2;
    private GameObject[] avatars3;
    private GameObject[] avatars4;


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
	    if (Input.GetKeyDown("left") )
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
}
