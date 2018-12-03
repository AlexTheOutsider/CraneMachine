﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockSpawner : MonoBehaviour
{
    public float waitingTime = 3f;
    public List<Collider2D> blockOnPlatform = new List<Collider2D>();
    
    private GameObject[] blockLibrary;   
    private Transform spawnPoint;

    private void Start()
    {
        blockLibrary = Resources.LoadAll<GameObject>("Prefabs/Blocks");
        spawnPoint = GameObject.Find("SpawnPoints").transform.GetChild(0);
        Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPoint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!blockOnPlatform.Contains(other) && other.CompareTag("Crate") &&
            !other.gameObject.GetComponent<Scorer>().isHooked)
        {
            blockOnPlatform.Add(other);
            //StartCoroutine(SpawnBlock(other));
            Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPoint);
        }
    }

    IEnumerator SpawnBlock(Collider2D other)
    {
        while (other.gameObject.GetComponent<Scorer>().isHooked)
        {
            yield return new WaitForSeconds(waitingTime);
        }

        //yield return new WaitForSeconds(waitingTime);
        Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPoint);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }
}