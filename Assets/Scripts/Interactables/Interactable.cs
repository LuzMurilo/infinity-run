using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Holds information about interactables that will spawn. Should be included on every spawnable prefab.
public class Interactable : MonoBehaviour
{
    [SerializeField] private int[] possibleLanes = {0, 1, -1};
    public int[] Lanes {get{
        return possibleLanes;
    }
    private set{}}

    public float Length {get {
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        float firstItem = 0f;
        float lastItem = 0f;
        foreach (Transform child in children)
        {
            firstItem = Mathf.Min(firstItem, child.position.z);
            lastItem = Math.Max(lastItem, child.position.z);
        }
        return lastItem - firstItem;
    }
    private set{}}
}
