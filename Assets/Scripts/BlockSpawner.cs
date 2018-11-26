using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public float waitingTime = 3f;

    private GameObject[] blockLibrary;
    private Transform spawnPoint;
    readonly List<Collider2D> blockOnPlatform = new List<Collider2D>();
    
    private void Start()
    {
        blockLibrary = Resources.LoadAll<GameObject>("Prefabs/Blocks");
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPoint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!blockOnPlatform.Contains(other) && other.CompareTag("Crate")&&!other.gameObject.GetComponent<Scorer>().isHooked)
        {       
            //StartCoroutine(SpawnBlock(other));
            blockOnPlatform.Add(other);
            Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPoint);
        }
    }

    IEnumerator SpawnBlock(Collider2D other)
    {
/*        while (other.gameObject.GetComponent<Scorer>().isHooked)
        {
            yield return new WaitForSeconds(waitingTime);
        }*/
        yield return new WaitForSeconds(waitingTime);
        Instantiate(blockLibrary[Random.Range(0, blockLibrary.Length)], spawnPoint);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }
}