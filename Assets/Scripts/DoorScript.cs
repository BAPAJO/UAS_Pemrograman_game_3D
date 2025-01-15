using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    public static bool doorKey;
    public bool open;
    public bool close;
    public bool inTrigger;
    public string sceneName;

    void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    void Update()
{
    if (inTrigger)
    {
        Debug.Log("Player is in trigger zone.");

        if (doorKey)
        {
            Debug.Log("Door key is available.");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F pressed. Triggering scene...");
                SceneManager.LoadScene(sceneName);
                open = true;
                close = false;
            }
        }
        else
        {
            Debug.Log("Door key not available.");
        }
    }
}

    void OnGUI()
    {
        if (inTrigger)
        {
            if (open)
            {
                GUI.Box(new Rect(0, 0, 200, 25), "Press F to close");
            }
            else
            {
                if (doorKey)
                {
                    GUI.Box(new Rect(0, 0, 200, 25), "Press F to open");
                }
                else
                {
                    GUI.Box(new Rect(0, 0, 200, 25), "Cari Semua Soal!");
                }
            }
        }
    }
}