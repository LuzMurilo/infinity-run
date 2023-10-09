using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent<ICollidable>(out ICollidable collider))
        {
            collider.OnCollideWithPlayer(playerManager);
        }
    }
}
