using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    /*This class replaces the Input Manager by default in order to customize our own inputs*/

    public static GameInputManager GIM;

    public KeyCode left {get; set;}
    public KeyCode right {get; set;}
    public KeyCode jump {get; set;}
    public KeyCode fire {get; set;}
    public KeyCode specialAction {get; set;}
    public KeyCode switchWeapon {get; set;}
    public KeyCode cancel {get; set;}
    public KeyCode take {get; set;}
    public KeyCode drop {get; set;}


    void Awake()
    {
        if (GIM == null)
        {
            DontDestroyOnLoad(gameObject);
            GIM = this;
        }
        else if (GIM != this)
        {
            Destroy(gameObject);
        }

        UpdateInputs();

    }

    public static void UpdateInputs()
    {
        GIM.left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "Q"));
        GIM.right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        GIM.jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        GIM.fire = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", "Mouse0"));
        GIM.specialAction = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpecialAction", "Mouse1"));
        GIM.switchWeapon = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SwitchWeapon", "Mouse2"));
        GIM.cancel = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Cancel", "Escape"));
        GIM.take = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Take", "F"));
        GIM.drop = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Drop", "C"));
    }

}
