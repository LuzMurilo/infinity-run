using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds information about interactables that will spawn. Should be included on every spawnable prefab.
public class Interactable : MonoBehaviour
{
    [SerializeField] private int[] possibleLanes = {0, 1, -1};
    public int[] Lanes {get{
        return possibleLanes;
    }
    private set{
        
    }}
}
