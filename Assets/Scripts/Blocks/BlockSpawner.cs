using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;
    private List<Block> blocks;

    private void Awake() 
    {
        blocks = new List<Block>();
    }

    private void Start() 
    {
        blocks.Add(Instantiate<Block>(blockPrefab, transform.position, transform.rotation, transform));
    }
}
