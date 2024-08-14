using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private int maxLife;
    
    public int currentLife {get; private set;}
    public int totalCoins {get; private set;}
    public int blocksTraveled {get; private set;}
    public bool isInvulnerable {get; private set;}
    public bool isDead {get; private set;}

    public UnityEvent<PlayerManager> OnPlayerDeath; //this
    public UnityEvent OnTakeHit;
    public UnityEvent<Block> OnEnterBlock;

    public void StartPlayer()
    {
        isInvulnerable = false;
        isDead = false;
        totalCoins = 0;
        blocksTraveled = 0;
        currentLife = maxLife;
        UIManager.Singleton.SetHeartsDisplayed(currentLife);
    }

    public void Die()
    {
        if (isDead) return;
        Debug.Log("[Player] Player Died!");
        isDead = true;
        OnPlayerDeath.Invoke(this);
    }

    public void TakeHit()
    {
        if (isInvulnerable || isDead) return;
        Debug.Log("Took hit!");
        currentLife--;
        UIManager.Singleton.SetHeartsDisplayed(currentLife);
        if (currentLife <= 0)
        {
            Die();
            return;
        }
        OnTakeHit.Invoke();
        StartCoroutine(InvulnerableFrame(1.0f));
    }

    public void GiveCoins(int coins)
    {
        totalCoins += coins;
        UIManager.Singleton.SetCoinDisplayNumber(totalCoins);
    }

    public void EnteredNewBlock(Block block)
    {
        blocksTraveled += 1;
        OnEnterBlock.Invoke(block);
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
