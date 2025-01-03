using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    public GameObject menu;
    public string sceneName;
    public bool isShowing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doorUnlock(){
        DoorScript.doorKey = true;
        isShowing = false;
        menu.SetActive(isShowing);
    }
    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
