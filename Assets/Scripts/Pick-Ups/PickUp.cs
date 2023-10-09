using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, ICollidable
{
    [SerializeField] private PickUpSO pickupData;
    public void OnCollideWithPlayer(PlayerManager player)
    {
        player.GiveCoins(pickupData.value);
        Destroy(gameObject);
    }
}
