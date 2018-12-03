using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private GameManager gameManager;
    private BlockSpawner blockSpawner;
    private GameObject[] blockLibrary;   
    private Transform spawnPoint;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        blockSpawner = GameObject.Find("PlatformTrigger").GetComponent<BlockSpawner>();
        blockLibrary = Resources.LoadAll<GameObject>("Prefabs/Blocks");
        spawnPoint = GameObject.Find("SpawnPoints").transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Crate"))
        {
            if (!blockSpawner.blockOnPlatform.Contains(other))
            {
                Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPoint);
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