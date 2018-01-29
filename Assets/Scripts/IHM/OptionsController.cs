using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    private Dictionary<string, KeyCode> codes = new Dictionary<string, KeyCode>();

    public Text left, right, jump, fire, action, switchWeapon, cancel;

    private GameObject currentKey;

    void Start()
    {
        Cursor.visible = true;

        codes.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left","W")));
        codes.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        codes.Add("Fire", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire", "Mouse0")));
        codes.Add("SpecialAction", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SpecialAction", "Mouse1")));
        codes.Add("SwitchWeapon", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SwitchWeapon", "Mouse2")));
        codes.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        codes.Add("Cancel", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Cancel", "Escape")));

        left.text = codes["Left"].ToString();
        right.text = codes["Right"].ToString();
        jump.text = codes["Jump"].ToString();
        fire.text = codes["Fire"].ToString();
        action.text = codes["SpecialAction"].ToString();
        switchWeapon.text = codes["SwitchWeapon"].ToString();
        cancel.text = codes["Cancel"].ToString();

    }

    public void Save()
    {
        foreach (var key in codes)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
        ExitSettings();
    }

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

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
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
    
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;

    }

}
