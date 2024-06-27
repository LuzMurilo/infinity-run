using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerManager player;
    void Start()
    {
        Debug.Log("[GM] Game Started!");
        player.StartPlayer();
    }
}
