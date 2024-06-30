using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton {get; private set;}
    [SerializeField] PlayerManager player;

    private void Awake() 
    {
        if (GameManager.Singleton != null)
        {
            Destroy(this);
            return;
        }
        GameManager.Singleton = this;
    }
    void Start()
    {
        Debug.Log("[GM] Game Started!");
        player.StartPlayer();
    }
}
