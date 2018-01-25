using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Static class to give parameters to a level from the menu
public class LevelParam
{
    //Object where we stock string
    private static Dictionary<string,string> _params = new Dictionary<string, string>();

    // Get a string parameter
    public static string Get (string key)
    {
        if (_params.ContainsKey(key))
            return _params[key];
        return null;
	}

    // Set a string parameter
    public static void Set (string key, string value)
    {
        if (_params.ContainsKey(key))
            _params[key] = value;
        else
            _params.Add(key, value);
    }

    // Clear all parameters
    public static void Clear ()
    {
        _params.Clear();
    }
}
