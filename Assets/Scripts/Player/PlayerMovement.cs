using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] public bool isRunning;
    [SerializeField] private List<LaneController> lanes;
    private Dictionary<int, Transform> lanesTransforms;
    public int currentLane;

    private void Awake() 
    {
        lanesTransforms = new Dictionary<int, Transform>();
    }

    private void Start() 
    {
        isRunning = true;
        currentLane = 0;
        lanes.ForEach(lane => lanesTransforms.Add(lane.index, lane.transform));
    }

    private void FixedUpdate() 
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!isRunning) return;

        if (transform.position.x != lanesTransforms[currentLane].position.x)
        {
            transform.position += new Vector3(lanesTransforms[currentLane].position.x - transform.position.x, 0, 0);
        }

        transform.Translate(Vector3.forward * Time.fixedDeltaTime * forwardSpeed, Space.World);
    }

    public void ChangeToLane(int newLaneIndex)
    {
        if (newLaneIndex == currentLane) return;
        if (!lanesTransforms.ContainsKey(newLaneIndex)) return;

        currentLane = newLaneIndex;
    }

    public void MoveRight()
    {
        ChangeToLane(currentLane + 1);
    }
    public void MoveLeft()
    {
        ChangeToLane(currentLane - 1);
    }
}
