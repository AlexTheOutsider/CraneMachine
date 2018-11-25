using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    private bool hasBlock = true;
    private float waitingTime = 0f;
    public float spawnTime = 3f;
    private int numberOfColliders = 0;

    List<Collision2D> collidedObjects = new List<Collision2D>();
    
    //public List<GameObject> blockList;    
    public GameObject[] blockListArray;

    private void Start()
    {
        blockListArray = Resources.LoadAll<GameObject>("Prefabs");
        //blockList = blockListArray.ToList();
    }

    private void Update()
    {
        print(numberOfColliders);
        numberOfColliders = collidedObjects.Count;
        if (numberOfColliders == 0)
        {
            waitingTime += Time.deltaTime;
            if (waitingTime > spawnTime)
            {
                GameObject randomBlock = blockListArray[Random.Range(0, blockListArray.Length)];
                Instantiate(randomBlock);
                waitingTime = 0f;
            }
            collidedObjects.Clear();
            return;
        }
        waitingTime = 0f;
        collidedObjects.Clear();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!collidedObjects.Contains(other) && other.collider.tag == "Crate")
        {
            collidedObjects.Add(other);
            //print("add");
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!collidedObjects.Contains(other) && other.collider.tag == "Crate")
        {
            collidedObjects.Add(other);
            //print("stay");
        }
    }
}