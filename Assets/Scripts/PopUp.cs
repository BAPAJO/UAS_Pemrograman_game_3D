using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MenuAppearScript : MonoBehaviour {
 
    public GameObject menu; // Assign in inspector
    public bool isShowing = false;
    private bool hasRun = false;

    void Update() {
        if (hasRun == false){
            isShowing = false;
            menu.SetActive(isShowing);
            hasRun = true;
        }
        else{
            if (Input.GetKeyDown("escape")) {
                if (isShowing == false){
                    isShowing = true;
                    menu.SetActive(isShowing);
                }

                else if (isShowing == true){
                    isShowing = false;
                    menu.SetActive(isShowing);
                }
            }
        }
    }
}