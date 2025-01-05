using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float walkSpeed = 12f;  // Speed while walking
    public float sprintSpeed = 18f; // Speed while sprinting
    private float currentSpeed;    // Variable to hold the current speed

    public float gravity = -9.81f;

    Vector3 velocity;

    // Stamina variables
    public float maxStamina = 5f;    // Maximum stamina in seconds
    private float currentStamina;
    public float staminaRegenRate = 1f; // Rate at which stamina regenerates
    public float staminaDepleteRate = 1f; // Rate at which stamina depletes
    private bool isSprinting = false;

    // Stamina regeneration delay
    public float regenDelay = 2f; // Time in seconds before regeneration starts
    private float regenCooldown;
    private bool shiftReleased = false; // Track if shift key has been released

    void Start()
    {
        currentSpeed = walkSpeed; // Start with walking speed
        currentStamina = maxStamina; // Initialize stamina to max
        regenCooldown = 0f; // No cooldown initially
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleStamina();
    }

    void HandleMovement()
    {
        // Check for sprint input and stamina availability
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            currentSpeed = sprintSpeed;
            isSprinting = true;
            shiftReleased = false; // Reset shift release tracker
            regenCooldown = regenDelay; // Reset cooldown when sprinting
        }
        else
        {
            currentSpeed = walkSpeed;
            isSprinting = false;

            // Check if Shift key was released
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                shiftReleased = true; // Mark that the sprint key was released
            }
        }

        // Get movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Move the player
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleStamina()
    {
        if (isSprinting)
        {
            // Deplete stamina while sprinting
            currentStamina -= staminaDepleteRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
        else if (shiftReleased)
        {
            // Countdown the regen cooldown after shift is released
            if (regenCooldown > 0)
            {
                regenCooldown -= Time.deltaTime;
            }
            else
            {
                // Regenerate stamina after cooldown
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
        }
    }

    void OnGUI()
    {
        // Display stamina bar on the screen
        GUI.color = Color.green;
        GUI.HorizontalScrollbar(new Rect(10, 10, 200, 20), 0, currentStamina, 0, maxStamina);
    }
}
