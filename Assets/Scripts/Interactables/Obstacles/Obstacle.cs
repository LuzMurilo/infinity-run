using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollidable
{
    [SerializeField] private ObstacleSO obstacleData;
    public ObstacleSO Data {get{
        return obstacleData;
    } private set{
        Debug.LogWarning("Can't set obstacle data on runtime!");
    }}
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
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }

        isDestroyed = true;
    }
}
