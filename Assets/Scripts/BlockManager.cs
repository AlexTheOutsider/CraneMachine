using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public float spawnTime = 3f;

    private Transform spawnPoint;
    private int blockNum = 0;
    private float waitingTime = 0f;
    readonly List<Collision2D> blockOnGround = new List<Collision2D>();
    private GameObject[] blockLibrary;

    private void Start()
    {
        blockLibrary = Resources.LoadAll<GameObject>("Prefabs/Blocks");
        spawnPoint = GameObject.Find("SpawnPoint").transform;
    }

    private void Update()
    {
        blockNum = blockOnGround.Count;
        if (blockNum == 0)
        {
            waitingTime += Time.deltaTime;
            if (waitingTime > spawnTime)
            {
                Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPoint);
                waitingTime = 0f;
            }

            blockOnGround.Clear();
            return;
        }

        waitingTime = 0f;
        blockOnGround.Clear();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!blockOnGround.Contains(other) && other.collider.tag == "Crate")
        {
            blockOnGround.Add(other);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        OnCollisionEnter2D(other);
    }
}