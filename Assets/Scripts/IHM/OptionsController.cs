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
        codes.Add("Left", KeyCode.Q);
        codes.Add("Right", KeyCode.D);
        codes.Add("Fire", KeyCode.Mouse0);
        codes.Add("SpecialAction", KeyCode.Mouse1); //Specific actions with some weapons
        codes.Add("SwitchWeapon", KeyCode.Mouse2); 
        codes.Add("Jump", KeyCode.Space);
        codes.Add("Cancel", KeyCode.Escape);

        left.text = codes["Left"].ToString();
        right.text = codes["Right"].ToString();
        jump.text = codes["Jump"].ToString();
        fire.text = codes["Fire"].ToString();
        action.text = codes["SpecialAction"].ToString();
        switchWeapon.text = codes["SwitchWeapon"].ToString();
        cancel.text = codes["Cancel"].ToString();
    }


    // Permit to acceed to the scene in paramater
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
        }
    }
    
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;

    }

}
