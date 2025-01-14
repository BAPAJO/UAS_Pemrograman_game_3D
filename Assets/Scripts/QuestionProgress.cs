using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionProgress : MonoBehaviour
{

    public TextMeshProUGUI progressText; // Referensi ke teks soal progress
    private int soalBenar = 0; // Jumlah soal yang dijawab benar
    private int totalSoal = 10; // Total soal yang tersedia
    // Start is called before the first frame update
    void Start()
    {
        UpdateProgressText(); // Inisialisasi teks di awal
        
    }

    public void IncrementSoalBenar()
    {
        if (soalBenar < totalSoal)
        {
            soalBenar++; // Tambah jumlah soal yang dijawab benar
            UpdateProgressText(); // Perbarui teks
        }
    }

    // Fungsi untuk memperbarui teks progress
    private void UpdateProgressText()
    {
        progressText.text = $"{soalBenar}/{totalSoal}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
