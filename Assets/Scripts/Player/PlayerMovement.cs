using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 2.0f;
    [SerializeField] private float slowedForwardSpeed = 1.0f;
    [SerializeField] private float currentForwardSpeed;
    [SerializeField] private float speedIncreasePerBlock;
    private float addedSpeed;
    [SerializeField] private float sideSpeed = 10.0f;
    [SerializeField] private float distanceBias = 0.01f;
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private float jumpTime = 1.0f;
    private float jumpTargetHeight = 0.0f;
    private float jumpElapsedTime = 0.0f;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayers;
    public Block currentBlock {get; private set;}
    private Dictionary<int, Transform> lanesTransforms;
    public int currentLane;

    // BOOLS
    [SerializeField] public bool isRunning {get; private set;}
    [SerializeField] public bool isGrounded {get; private set;}
    [SerializeField] public bool isJumping {get; private set;}
    [SerializeField] public bool isSlowed {get; private set;}


    private void Awake() 
    {
        lanesTransforms = new Dictionary<int, Transform>();
    }

    private void Start() 
    {
        isRunning = false;
        isJumping = false;
        isGrounded = true;
        isSlowed = false;
        currentLane = 0;
    }

    private void FixedUpdate() 
    {
        HandleMovement();
    }

    public void StartMovement()
    {
        isRunning = true;
        isSlowed = false;
        currentForwardSpeed = forwardSpeed;
        addedSpeed = 0;
    }

    // CONTROLS
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

    // INTERNALS
    private void HandleMovement()
    {
        if (!isRunning || currentBlock == null) return;
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

        if (isSlowed)
        {
            currentForwardSpeed = slowedForwardSpeed;
        }
        else if (currentForwardSpeed < forwardSpeed + addedSpeed) // If player is slower than the desired speed, increment slowly
        {
            currentForwardSpeed += 5.0f * Time.deltaTime;
            if (currentForwardSpeed > forwardSpeed + addedSpeed) currentForwardSpeed = forwardSpeed + addedSpeed;
        }
        
        transform.Translate(currentBlock.direction * Time.fixedDeltaTime * currentForwardSpeed, Space.World);
    }

    public void NewBlock(Block newBlock)
    {
        lanesTransforms.Clear();
        currentBlock = newBlock;
        currentBlock.Lanes.Values.ToList().ForEach(lane => lanesTransforms.Add(lane.index, lane.transform));
        IncreaseSpeed(speedIncreasePerBlock);
    }

    public void StopMovement()
    {
        isRunning = false;
        isJumping = false;
    }

    public void SetSlowSpeed()
    {
        isSlowed = true;
    }
    public void SetNormalSpeed()
    {
        isSlowed = false;
    }

    public void IncreaseSpeed(float amount)
    {
        addedSpeed += amount;
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
            float newYPos = (jumpCurve.Evaluate(percentageComplete) * jumpHeight) + jumpTargetHeight - jumpHeight;
            transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
        }
        else
        {
            isJumping = false;
        }
    }

    
}
