using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollidable
{
    [SerializeField] private ObstacleSO obstacleData;
    [SerializeField] private MeshRenderer mesh;
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
            mesh.enabled = false;
        }

        isDestroyed = true;
    }
}
