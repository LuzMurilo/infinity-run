using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCollision playerCollision;

    public int totalCoins;
    public bool isInvulnerable;

    public UnityEvent OnPlayerDeath;
    public UnityEvent OnTakeHit;

    private void Start() 
    {
        isInvulnerable = false;
        totalCoins = 0;
    }

    public void Die()
    {
        Debug.Log("[Player] Player Died!");

        OnPlayerDeath.Invoke();
    }

    public void TakeHit()
    {
        if (isInvulnerable) return;
        Debug.Log("Took hit!");
        OnTakeHit.Invoke();
        StartCoroutine(InvulnerableFrame(1.0f));
    }

    public void GiveCoins(int coins)
    {
        totalCoins += coins;
        Debug.Log("[Player] got " + coins + " coins, total: " + totalCoins);
    }

    public void EnteredNewBlock(Block block)
    {
        playerMovement.NewBlock(block);
        Debug.Log("[Player] Entered a new block!");
    }

    private IEnumerator InvulnerableFrame(float seconds)
    {
        isInvulnerable = true;
        playerMovement.SetSlowSpeed();
        yield return new WaitForSeconds(seconds);
        isInvulnerable = false;
        playerMovement.SetNormalSpeed();
    }
}
