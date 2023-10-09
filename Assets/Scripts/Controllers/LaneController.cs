using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour
{
    public int index;
    public List<Transform> obstacles {get; private set;}

    private void Awake() 
    {
        obstacles = new List<Transform>();
    }

    private void Start() 
    {
        transform.GetComponentsInChildren<Transform>(false, obstacles);
        obstacles.Remove(transform);
    }
}
