using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 2.0f;
    [SerializeField] private float sideSpeed = 10.0f;
    [SerializeField] private float distanceBias = 0.01f;
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private float jumpTime = 1.0f;
    [SerializeField] public bool isJumping = false;
    private float jumpTargetHeight = 0.0f;
    private float jumpElapsedTime = 0.0f;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayers;
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
        isJumping = false;
        isGrounded = true;
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
        isGrounded = CheckGrounded();

        if (Mathf.Abs(transform.position.x - lanesTransforms[currentLane].position.x) > distanceBias)
        {
            Vector3 targetPosition = new Vector3(lanesTransforms[currentLane].position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, sideSpeed * Time.deltaTime);
        }
        else if (transform.position.x != lanesTransforms[currentLane].position.x)
        {
            transform.position = new Vector3(lanesTransforms[currentLane].position.x, transform.position.y, transform.position.z);
        }

        if (isJumping)
        {
            JumpMovement();
        }

        transform.Translate(Vector3.forward * Time.fixedDeltaTime * forwardSpeed, Space.World);
    }



    //// CONTROLS
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
    public void Jump()
    {
        if (isGrounded && isRunning)
        {
            isJumping = true;
            jumpTargetHeight = transform.position.y + jumpHeight;
            jumpElapsedTime = 0.0f;
        }
    }


    private bool CheckGrounded()
    {
        if (isJumping) return false;
        return Physics.CheckSphere(groundCheckTransform.position, 0.5f, groundLayers);
    }
    private void JumpMovement()
    {
        if (transform.position.y < jumpTargetHeight && jumpElapsedTime < jumpTime)
        {
            jumpElapsedTime += Time.fixedDeltaTime;
            float percentageComplete = jumpElapsedTime / jumpTime;
            float newYPos = (jumpCurve.Evaluate(percentageComplete) * (jumpHeight)) + jumpTargetHeight - jumpHeight;
            transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
        }
        else
        {
            isJumping = false;
        }
    }

    public void StopMovement()
    {
        isRunning = false;
        isJumping = false;
    }
}
