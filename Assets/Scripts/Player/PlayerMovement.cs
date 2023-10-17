using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 2.0f;
    [SerializeField] private float sideSpeed = 10.0f;
    [SerializeField] private float laneDistanceBias = 0.01f;
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

        if (Mathf.Abs(transform.position.x - lanesTransforms[currentLane].position.x) > laneDistanceBias)
        {
            Vector3 targetPosition = new Vector3(lanesTransforms[currentLane].position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, sideSpeed * Time.deltaTime);
        }
        else if (transform.position.x != lanesTransforms[currentLane].position.x)
        {
            transform.position = new Vector3(lanesTransforms[currentLane].position.x, transform.position.y, transform.position.z);
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
