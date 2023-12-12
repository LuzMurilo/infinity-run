using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Vector2 lengthLimits;
    [SerializeField] private Vector2 angleLimits;
    [SerializeField] private Transform start;
    [SerializeField] private Transform finish;
    [SerializeField] private Transform ground;
    public float length {get; private set;}
    public float angle {get; private set;}

    private void Awake() 
    {
        length = Mathf.Floor(Random.Range(lengthLimits.x, lengthLimits.y));
        angle = Random.Range(angleLimits.x, angleLimits.y);
        ground.localScale = new Vector3(ground.localScale.x, ground.localScale.y, length);
        ground.localPosition = new Vector3(0, 0, length / 2);
        finish.localPosition = new Vector3(0, 0, length);
    }
}
