using UnityEngine;
using System.Collections;

public class cmd_joinannounce : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        string[] cmdlineargs = System.Environment.GetCommandLineArgs();
        foreach (string line in cmdlineargs)
        {
            Debug.Log("cmdline: " + line);
            if (line.StartsWith("-announcement="))
            {
                string announcement = line.Remove(0, "-announcement=".Length);
                gameObject.GetComponent<TextMesh>().text += announcement;
            }
        }
    }
}
