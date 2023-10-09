using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCollision playerCollision;

    public int totalCoins = 0;

    public void Die()
    {
        Debug.Log("[Player] Player Died!");
        if (playerMovement)
        {
            playerMovement.isRunning = false;
        }
    }

    public void GiveCoins(int coins)
    {
        totalCoins += coins;
        Debug.Log("[Player] got " + coins + " coins, total: " + totalCoins);
    }
}
