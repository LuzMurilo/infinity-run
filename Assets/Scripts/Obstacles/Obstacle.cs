using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollidable
{
    [SerializeField] private ObstacleSO obstacleData;
    private bool isDestroyed = false;

    public void OnCollideWithPlayer(PlayerManager player)
    {
        if (isDestroyed) return;

        Debug.Log("Player collided with " + obstacleData.obstacleName);
        if (obstacleData.killPlayer)
        {
            player.Die();
        }
        else
        {
            player.TakeHit();
            GetComponent<MeshRenderer>().enabled = false;
        }

        isDestroyed = true;
    }
}
