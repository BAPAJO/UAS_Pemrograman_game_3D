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

    private bool notifTampil = false; // Menyimpan status apakah notifikasi sudah muncul
    public GameObject notifGate;     // Objek UI notifikasi




    void Start()
    {
        UpdateProgressText(); // Inisialisasi teks di awal
        
    }

    public void IncrementSoalBenar()
    {
        soalBenar++; // Tambah jumlah soal yang dijawab benar
        UpdateProgressText(); // Perbarui teks

        if (soalBenar == totalSoal && !notifTampil)
        {
            notifTampil = true; // Set status agar notifikasi hanya muncul sekali
            notifGate.SetActive(true); // Aktifkan notifikasi
            
            // Opsional: Nonaktifkan notifikasi setelah beberapa detik
            StartCoroutine(HilangkanNotif());
            
            //soalBenar++; // Tambah jumlah soal yang dijawab benar
            //UpdateProgressText(); // Perbarui teks
        }
    }

    // Fungsi untuk memperbarui teks progress
    private void UpdateProgressText()
    {
        progressText.text = $"{soalBenar}/{totalSoal}";
    }

    private IEnumerator HilangkanNotif()
    {
        yield return new WaitForSeconds(5f); // Notifikasi muncul selama 5 detik
        notifGate.SetActive(false);         // Nonaktifkan notifikasi
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
