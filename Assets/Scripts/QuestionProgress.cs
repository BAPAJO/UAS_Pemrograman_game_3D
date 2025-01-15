using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionProgress : MonoBehaviour
{
    public TextMeshProUGUI progressText; // Reference to progress text
    private int soalBenar = 0; // Number of correct answers
    private int totalSoal = 10; // Total questions
    private bool notifTampil = false; // Notification status
    public GameObject notifGate; // UI notification object
    public DoorScript doorScript; // Reference to DoorScript

    void Start()
    {
        UpdateProgressText(); // Initialize text
    }

    public void IncrementSoalBenar()
    {
        soalBenar++; // Increment correct answers
        UpdateProgressText(); // Update text

        if (soalBenar == totalSoal && !notifTampil)
        {
            notifTampil = true; // Notification only shows once
            notifGate.SetActive(true); // Activate notification

            // Unlock the door
            if (doorScript != null)
            {
                DoorScript.doorKey = true; // Unlock the door
                Debug.Log("Door unlocked!");
            }
            else
            {
                Debug.LogError("DoorScript reference is missing!");
            }

            // Optional: Hide the notification after a few seconds
            StartCoroutine(HilangkanNotif());
        }
    }

    private void UpdateProgressText()
    {
        progressText.text = $"{soalBenar}/{totalSoal}";
    }

    private IEnumerator HilangkanNotif()
    {
        yield return new WaitForSeconds(5f); // Notification appears for 5 seconds
        notifGate.SetActive(false); // Deactivate notification
    }
}
