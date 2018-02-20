using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] avatarList;
    private int index;

    private void Start()
    {
        avatarList = new GameObject[transform.childCount];

        //Fill the array
        for(int i=0; i<transform.childCount; i++)
        {
            avatarList[i] = transform.GetChild(i).gameObject;
        }

        //Toggle off the renderer
        foreach (GameObject go in avatarList)
        {
            go.SetActive(false);
        }

        //Toggle the first index
        if (avatarList[0])
            avatarList[0].SetActive(true);
    }

    public void ToggleAvatar(int direction)
    {
        //Toggle off the current avatar
        avatarList[index].SetActive(false);

        index += direction;
        if (index < 0)
            index = avatarList.Length - 1;
        if (index >= avatarList.Length)
            index = 0;

        //Toggle on the new avatar
        avatarList[index].SetActive(true);
    }
}
