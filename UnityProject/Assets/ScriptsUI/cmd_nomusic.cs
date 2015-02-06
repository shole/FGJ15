using UnityEngine;
using System.Collections;

public class cmd_nomusic : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        string[] cmdlineargs = System.Environment.GetCommandLineArgs();
        foreach (string line in cmdlineargs)
        {
            Debug.Log("cmdline: " + line);
            if (line.StartsWith("-nomusic"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
