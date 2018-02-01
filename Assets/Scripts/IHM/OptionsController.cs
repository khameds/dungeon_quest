using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    private Dictionary<string, KeyCode> codes = new Dictionary<string, KeyCode>();

    //The texts which are displayed on the button in the option menu
    public Text left, right, jump, fire, action, switchWeapon, cancel, take, drop;

    private GameObject currentKey;

    void Start()
    {
        Cursor.visible = true;

        //Dynamic display of the keybinds and mouse controls in the option menu
        codes.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left","Q")));
        codes.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        codes.Add("Fire", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", "Mouse0")));
        codes.Add("SpecialAction", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpecialAction", "Mouse1")));
        codes.Add("SwitchWeapon", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SwitchWeapon", "Mouse2")));
        codes.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        codes.Add("Cancel", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Cancel", "Escape")));
        codes.Add("Take", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Take", "F")));
        codes.Add("Drop", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Drop", "C")));

        left.text = codes["Left"].ToString();
        right.text = codes["Right"].ToString();
        jump.text = codes["Jump"].ToString();
        fire.text = codes["Fire"].ToString();
        action.text = codes["SpecialAction"].ToString();
        switchWeapon.text = codes["SwitchWeapon"].ToString();
        cancel.text = codes["Cancel"].ToString();
        take.text = codes["Take"].ToString();
        drop.text = codes["Drop"].ToString();

    }

    //Save the customized inputs in the PlayerPrefs file
    public void Save()
    {
        foreach (var key in codes)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
        GameInputManager.UpdateInputs();

        ExitSettings();
    }

    //Exit the Options scene to come back to the previous scene (main menu or pause menu)
    public void ExitSettings()
    {
        SceneManager.UnloadSceneAsync("options");
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ExitSettings();
        }

    }

    //Change the keybinds or mouse control
    void OnGUI()
    {
        //If we have clicked on a button to change the value of the keybind
        if (currentKey != null)
        {
            //We get the event the user create (click on his mouse or press a key)
            Event e = Event.current;

            //We add the new control to the Dictionnary codes to add this to the PlayerPrefs
            //And we change the text of the button
            if (e.isKey)
            {
                codes[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
            if (e.isMouse)
            {
                codes[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse" + e.button.ToString());
                currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse" + e.button.ToString();
                currentKey = null;
            }
        }
    }
    
    //Get the button which is clicked
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }

}
