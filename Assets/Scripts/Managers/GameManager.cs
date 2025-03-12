using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton {get; private set;}
    [SerializeField] private PlayerManager player;

    public int totalCoins;
    public int maxDistance;

    private void Awake() 
    {
        if (Singleton != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Singleton = this;
        DontDestroyOnLoad(this.gameObject);

        totalCoins = 0;
        maxDistance = 0;
    }
    void Start()
    {
        Debug.Log("[GM] Game Started!");
    }

    void Update()
    {
        if (player == null) 
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            if (player != null && !player.playerStarted) player.StartPlayer();
        }
    }

    public void RestartGame()
    {
        Debug.Log("[GM] Restarting Game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveRunInfo(PlayerManager player) {
        Debug.Log("[GM] saving run info");
        maxDistance = Math.Max(maxDistance, player.distanceTraveled);
        totalCoins += player.totalCoins;
        UIManager.Singleton.ShowGameOverScreen(player);
    }

}
