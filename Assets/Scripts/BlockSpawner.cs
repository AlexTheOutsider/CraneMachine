using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockSpawner : MonoBehaviour
{
    public float waitingTime = 3f;
    public List<Collider2D> blockOnPlatform = new List<Collider2D>();  
    public GameObject[] blockLibrary;   
    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    private void Start()
    {
        blockLibrary = Resources.LoadAll<GameObject>("Prefabs/Blocks");
        spawnPointLeft = GameObject.Find("SpawnPoints").transform.GetChild(0);
        spawnPointRight = GameObject.Find("SpawnPoints").transform.GetChild(1);
        Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPointLeft);
        Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPointRight);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!blockOnPlatform.Contains(other) && other.CompareTag("Crate") &&
            !other.gameObject.GetComponent<Scorer>().isHooked)
        {
            blockOnPlatform.Add(other);
            //StartCoroutine(SpawnBlock(other));
            if (other.transform.parent.name.Contains("Left"))
                Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPointLeft);
            else if (other.transform.parent.name.Contains("Right"))
                Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPointRight);
        }
    }

    IEnumerator SpawnBlock(Collider2D other)
    {
        while (other.gameObject.GetComponent<Scorer>().isHooked)
        {
            yield return new WaitForSeconds(waitingTime);
        }

        //yield return new WaitForSeconds(waitingTime);
        Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPointLeft);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }
}