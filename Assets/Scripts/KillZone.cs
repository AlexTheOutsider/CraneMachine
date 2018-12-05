using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private GameManager gameManager;
    private BlockSpawner blockSpawner;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        blockSpawner = GameObject.Find("PlatformTrigger").GetComponent<BlockSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crate"))
        {
            if (!blockSpawner.blockOnPlatform.Contains(other))
            {
                if (other.transform.parent.name.Contains("Left"))
                    Instantiate(blockSpawner.blockLibrary[Random.Range(0, blockSpawner.blockLibrary.Length)], blockSpawner.spawnPointLeft);
                else if (other.transform.parent.name.Contains("Right"))
                    Instantiate(blockSpawner.blockLibrary[Random.Range(0, blockSpawner.blockLibrary.Length)], blockSpawner.spawnPointRight);
            }
            else
            {
                blockSpawner.blockOnPlatform.Remove(other);
            }

            Destroy(other.gameObject);
            gameManager.TimerPunishment();
        }
    }
}