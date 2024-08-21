using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;
    [SerializeField] private int maxBlocks;
    public Queue<Block> blocks;
    private Vector3 nextSpawnPosition;
    private int spawnQueue = 0;

    private void Awake() 
    {
        blocks = new Queue<Block>();
        nextSpawnPosition = transform.position;
    }

    private void Start() 
    {
        SpawnNewBlock();
    }

    public void SpawnNewBlock()
    {
        spawnQueue++;
        if (spawnQueue == 1) InstantiateBlock();
        if (blocks.Count > maxBlocks) DeleteLastBlock();
    }

    private void InstantiateBlock()
    {
        blocks.Enqueue(Instantiate<Block>(blockPrefab, nextSpawnPosition, transform.rotation, transform));
    }

    public void DeleteLastBlock()
    {
        if (blocks.Count == 0)
        {
            if (spawnQueue == 0)
            {
                Debug.LogError("Trying to destroy blocks but the queue is empty!");
                return;
            }
            spawnQueue--;
            return;
        }
        GameObject blockToDelete = blocks.Peek().gameObject;
        Destroy(blockToDelete);
        blocks.Dequeue();
    }

    public void ChangeNextSpawnPositon(Vector3 position)
    {
        if (spawnQueue == 0) return;
        
        spawnQueue--;
        nextSpawnPosition = position;
        if (spawnQueue > 0)
        {
            InstantiateBlock();
        }
    }

}
