using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton {get; private set;}
    [SerializeField] private PlayerManager player;

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

    public void RestartGame()
    {
        Debug.Log("[GM] Restarting Game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
