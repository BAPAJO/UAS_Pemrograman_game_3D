using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssetSoal : MonoBehaviour
{

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

    // Start is called before the first frame update
    void Start()
    {
        soal = assetSoal.ToString().Split('#');

        soalSelesai = new bool[soal.Length];

        soalBag = new string[soal.Length,6];
        maxSoal = soal.Length;

        OlahSoal();

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
                continue;
            }
            continue;
        }

    }

    private void TampilkanSoal()
    {
        if (indexSoal < maxSoal)
        {
            if (ambilSoal)
            {
                for(int i=0; i < soal.Length; i++)
                {
                    int randomIndexSoal = Random.Range(0, soal.Length);
                    print("random: " + randomIndexSoal);
                    if (!soalSelesai[randomIndexSoal])
                    {
                        txtSoal.text = soalBag[randomIndexSoal, 0];
                        txtOpsiA.text = soalBag[randomIndexSoal, 1];
                        txtOpsiB.text = soalBag[randomIndexSoal, 2];
                        txtOpsiC.text = soalBag[randomIndexSoal, 3];
                        txtOpsiD.text = soalBag[randomIndexSoal, 4];
                        kunciJ = soalBag[randomIndexSoal,5][0];

                        soalSelesai[randomIndexSoal] = true;

                        ambilSoal = false;
                        break;

                    }
                    else
                    {
                        continue;
                    }
                }


            }
        }
    }

    public void Opsi(string opsiHuruf)
    {
        CheckJawaban(opsiHuruf[0]);
        indexSoal++;
        ambilSoal = true;
        TampilkanSoal();

    }

    private void CheckJawaban(char huruf)
    {
        if (huruf.Equals(kunciJ))
        {
            print("Benar");
        }
        else
        {
            print("Salah");
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
