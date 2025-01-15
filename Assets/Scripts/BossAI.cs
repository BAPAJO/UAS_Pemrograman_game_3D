using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, secondarySightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    public bool walking, chasing;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    public string deathScene;

    private bool playerInSecondaryRadius = false;
    private bool hasLeftSecondaryRadius = false;

    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
        // idleTime = Random.Range(2f, 5f);
        // chaseTime = Random.Range(8f, 15f);
    }

    void Update()
    {
        CheckForPlayer();

        if (chasing)
        {
            ChasePlayer();
        }
        else if (walking)
        {
            Patrol();
        }
    }

    void CheckForPlayer()
    {
        // Check if player is within the primary sight radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sightDistance);
        bool playerDetected = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("player"))
            {
                // Perform a raycast to check if the player is visible (no obstacles in the way)
                Vector3 directionToPlayer = (hitCollider.transform.position - transform.position).normalized;
                RaycastHit hit;

                if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightDistance))
                {
                    if (hit.collider.CompareTag("player"))
                    {
                        // The ray hit the player directly, no obstruction
                        walking = false;
                        StopCoroutine("stayIdle");
                        StopCoroutine("chaseRoutine");
                        StartCoroutine("chaseRoutine");
                        chasing = true;
                        playerDetected = true;
                        break;
                    }
                    else
                    {
                        // If the ray hit something other than the player, block sight (obstructed by a wall)
                        playerDetected = false;
                    }
                }
            }
        }

        // Check if the player is within the secondary sight radius
        Collider[] secondaryHitColliders = Physics.OverlapSphere(transform.position, secondarySightDistance);
        playerInSecondaryRadius = false;

        foreach (var hitCollider in secondaryHitColliders)
        {
            if (hitCollider.CompareTag("player"))
            {
                // Perform a raycast to check if the player is visible within the secondary radius
                Vector3 directionToPlayer = (hitCollider.transform.position - transform.position).normalized;
                RaycastHit hit;

                if (Physics.Raycast(transform.position, directionToPlayer, out hit, secondarySightDistance))
                {
                    if (hit.collider.CompareTag("player"))
                    {
                        // The ray hit the player directly, no obstruction
                        playerInSecondaryRadius = true;
                        break;
                    }
                    else
                    {
                        // If the ray hit something other than the player, block sight
                        playerInSecondaryRadius = false;
                    }
                }
            }
        }

        // If the player leaves the secondary radius, reduce chase time
        if (!playerInSecondaryRadius && chasing && !hasLeftSecondaryRadius)
        {
            chaseTime = Mathf.Clamp(chaseTime - 4f, minChaseTime, maxChaseTime);
            hasLeftSecondaryRadius = true;
        }

        if (playerInSecondaryRadius)
        {
            hasLeftSecondaryRadius = false;
        }
    }



    void Patrol()
    {
        dest = currentDest.position;
        ai.destination = dest;
        ai.speed = walkSpeed;
        aiAnim.ResetTrigger("sprint");
        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("walk");

        if (ai.remainingDistance <= ai.stoppingDistance)
        {
            aiAnim.ResetTrigger("sprint");
            aiAnim.ResetTrigger("walk");
            aiAnim.SetTrigger("idle");
            ai.speed = 0;
            StopCoroutine("stayIdle");
            StartCoroutine("stayIdle");
            walking = false;
        }
    }

    void ChasePlayer()
    {
        dest = player.position;
        ai.destination = dest;
        ai.speed = chaseSpeed;
        aiAnim.ResetTrigger("walk");
        aiAnim.ResetTrigger("idle");
        aiAnim.SetTrigger("sprint");

        float distance = Vector3.Distance(player.position, ai.transform.position);
        if (distance <= catchDistance)
        {
            player.gameObject.SetActive(false);
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("jumpscare");
            StartCoroutine(deathRoutine());
            chasing = false;
        }
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the primary detection radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightDistance);

        // Visualize the secondary detection radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, secondarySightDistance);

        // Visualize the raycast for line of sight
        if (player != null)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, directionToPlayer * sightDistance);
        }

        // Visualize the raycast for secondary line of sight
        if (player != null)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, directionToPlayer * secondarySightDistance);
        }
    }
}
