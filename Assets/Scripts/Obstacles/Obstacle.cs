using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollidable
{
    [SerializeField] private ObstacleSO obstacleData;

    public void OnCollideWithPlayer(PlayerManager player)
    {
        Debug.Log("Player collided with " + obstacleData.obstacleName);
        if (obstacleData.killPlayer)
        {
            player.Die();
        }
    }
}
