using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, ICollidable
{
    [SerializeField] private PickUpSO pickupData;
    public PickUpSO Data {get{
        return pickupData;
    } private set{
        Debug.LogWarning("Can't set pick-up data on runtime!");
    }}
    public void OnCollideWithPlayer(PlayerManager player)
    {
        player.GiveCoins(pickupData.value);
        Destroy(gameObject);
    }
}
