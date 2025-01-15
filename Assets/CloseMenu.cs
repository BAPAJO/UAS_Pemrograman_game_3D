using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuButtonScript : MonoBehaviour
{
    public GameObject menu; // Assign in inspector

    // This method will be called when the button is clicked
    public void CloseMenu()
    {
        menu.SetActive(false); // Deactivates the menu
    }
}
