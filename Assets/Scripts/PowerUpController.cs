using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public float speedBoost = 2f; 
    public float duration = 3f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(ApplySpeedBoost(playerMovement));
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator ApplySpeedBoost(PlayerMovement playerMovement)
    {
        float originalWalkSpeed = playerMovement.walkSpeed;
        float originalSprintSpeed = playerMovement.sprintSpeed;

        playerMovement.walkSpeed *= speedBoost;
        playerMovement.sprintSpeed *= speedBoost;

        yield return new WaitForSeconds(duration);

        playerMovement.walkSpeed = originalWalkSpeed;
        playerMovement.sprintSpeed = originalSprintSpeed;
    }
}

