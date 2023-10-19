using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCollision playerCollision;

    public int totalCoins = 0;

    public UnityEvent OnPlayerDeath;
    public UnityEvent OnTakeHit;

    public void Die()
    {
        Debug.Log("[Player] Player Died!");

        OnPlayerDeath.Invoke();
    }

    public void TakeHit()
    {
        OnTakeHit.Invoke();
    }

    public void GiveCoins(int coins)
    {
        totalCoins += coins;
        Debug.Log("[Player] got " + coins + " coins, total: " + totalCoins);
    }
}
