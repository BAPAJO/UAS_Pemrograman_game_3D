using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssetSoal : MonoBehaviour
{

    

    public GameObject menu;
    public bool isShowing = false;

    public TextAsset assetSoal;
    private string[] soal;
    private string[,] soalBag;
    int indexSoal;
    int maxSoal;
    bool ambilSoal;
    char kunciJ;

    bool[] soalSelesai;

    public Text txtSoal;
    public TextMeshProUGUI txtOpsiA, txtOpsiB, txtOpsiC, txtOpsiD;

    bool isHasil;
    private float durasi;
    public float durasiPenilaian;

    private List<int> soalBelumTerjawab = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        durasi = durasiPenilaian;


        soal = assetSoal.ToString().Split('#');

        soalSelesai = new bool[soal.Length];

        soalBag = new string[soal.Length,6];
        maxSoal = soal.Length;

        OlahSoal();
        InisialisasiSoalBelumTerjawab();

        ambilSoal = true;

        TampilkanSoal();

        print(soal[0]);
        
    }

    private void OlahSoal()
    {
        for(int i = 0; i < soal.Length; i++)
        {
            string[] tempSoal = soal[i].Split('+');
            for(int j = 0; j < tempSoal.Length; j++)
            {
                soalBag[i,j] = tempSoal[j];
                //continue;
            }
            //continue;
        }

    }

    private void InisialisasiSoalBelumTerjawab()
    {
        for (int i = 0; i < maxSoal; i++)
        {
            soalBelumTerjawab.Add(i);
        }
    }

    private void buatSoal()
    {
        int randomIndex = Random.Range(0, soalBelumTerjawab.Count);
        int soalID = soalBelumTerjawab[randomIndex];

        txtSoal.text = soalBag[soalID, 0];
        txtOpsiA.text = soalBag[soalID, 1];
        txtOpsiB.text = soalBag[soalID, 2];
        txtOpsiC.text = soalBag[soalID, 3];
        txtOpsiD.text = soalBag[soalID, 4];
        kunciJ = soalBag[soalID, 5][0];

        soalBelumTerjawab.RemoveAt(randomIndex);
    }

    private void TampilkanSoal()
    {
        if (indexSoal < maxSoal && soalBelumTerjawab.Count > 0)
        {
            if (ambilSoal)
            {
                //for(int i=0; i < soal.Length; i++)
                //{
                    //int randomIndexSoal = Random.Range(0, soalBelumTerjawab.Count);
                    buatSoal();
                    ambilSoal = false;

                    //print("random: " + randomIndexSoal);
                    //if (!soalSelesai[randomIndexSoal])
                    //{
                        //txtSoal.text = soalBag[randomIndexSoal, 0];
                        //txtOpsiA.text = soalBag[randomIndexSoal, 1];
                        //txtOpsiB.text = soalBag[randomIndexSoal, 2];
                       // txtOpsiC.text = soalBag[randomIndexSoal, 3];
                       // txtOpsiD.text = soalBag[randomIndexSoal, 4];
                       // kunciJ = soalBag[randomIndexSoal,5][0];

                        //soalSelesai[randomIndexSoal] = true;

                        //ambilSoal = false;
                        //break;
                }

                }
                else
                {
                    Debug.Log("Semua soal telah selesai!");
                    //continue;
                }
            }


            //}
        
    //}

    public GameObject panelSalah;

    public void Opsi(string opsiHuruf)
    {
        CheckJawaban(opsiHuruf[0]);
        
        //indexSoal++;
        //ambilSoal = true;
        //TampilkanSoal();

    }

    private void CheckJawaban(char huruf)
    {
        string penilaian;


        if (huruf.Equals(kunciJ))
        {
            Debug.Log("Jawaban benar!");
            penilaian = "Benar!";

            GameObject.Find("SetProgressQuestion").GetComponent<QuestionProgress>().IncrementSoalBenar(); // Tambahkan progres
            
            menu.SetActive(false);
            isShowing = false;

            buatSoal();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Debug.Log("Jawaban salah!");
            penilaian = "Salah!";
            buatSoal();

            if (isHasil)
            {
                
            }
            else
            {
                panelSalah.SetActive(true);
            }

            //panelSalah.SetActive(true);
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (panelSalah.activeSelf)
        {
            durasiPenilaian -= Time.deltaTime;

            if (durasiPenilaian <= 0)
            {
                panelSalah.SetActive(false);
                durasiPenilaian = durasi;

                TampilkanSoal();
            }
        }
        
    }
}
