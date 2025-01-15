using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light flashlight;
    // public KeyCode toggleKey = KeyCode.F;

    private bool isOn = true;

    void Start()
    {
        if (flashlight == null)
        {
            Debug.LogError("Flashlight not assigned!");
        }
    }

    void Update()
    {
        // if (Input.GetKeyDown(toggleKey))
        // {
        //     isOn = !isOn;
        //     flashlight.enabled = isOn;
        // }
    }
}


