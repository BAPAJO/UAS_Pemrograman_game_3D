using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    public GameObject menu;
    public string sceneName;
    public bool isShowing = false;


    public static QuestionManager Instance; // Singleton untuk akses global

    public List<int> unansweredQuestions = new List<int>(); // Indeks soal yang belum dijawab
    private int totalQuestions = 10; // Total soal

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Inisialisasi daftar soal
        for (int i = 0; i < totalQuestions; i++)
        {
            unansweredQuestions.Add(i);
        }
    }

    // Fungsi untuk mengambil soal acak
    public int GetRandomQuestion()
    {
        if (unansweredQuestions.Count > 0)
        {
            int randomIndex = Random.Range(0, unansweredQuestions.Count);
            int questionID = unansweredQuestions[randomIndex];
            unansweredQuestions.RemoveAt(randomIndex); // Hapus dari daftar
            return questionID;
        }
        else
        {
            Debug.LogWarning("Semua soal telah dijawab!");
            return -1; // Tidak ada soal tersisa
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void doorUnlock()
{
    Debug.Log("Unlocking the door...");
    DoorScript.doorKey = true;
    isShowing = false;
    menu.SetActive(isShowing);
}

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
