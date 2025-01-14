using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionPickUp : MonoBehaviour
{
    public bool inTrigger;
    public GameObject menu;
    public bool isShowing = false;
    private bool hasRun = false;

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
        if (hasRun == false){
            isShowing = false;
            menu.SetActive(isShowing);
            hasRun = true;
        }
        else{
            if (inTrigger)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isShowing = true;
                    menu.SetActive(isShowing);
                    // gameObject.GetComponent<MouseLook>().questionOpen = true;

                    Cursor.lockState = CursorLockMode.None; // Bebaskan kursor
                    Cursor.visible = true;
                    
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void OnGUI()
    {
        if (inTrigger)
        {
            GUI.Box(new Rect(0, 60, 200, 25), "Press F to See Question");
        }
    }
}