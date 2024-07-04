using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float angularSpeed;
    [SerializeField] private PlayerMovement playerMovement;

    private void LateUpdate() 
    {
        float currentAngle = transform.eulerAngles.x;
        if (currentAngle > 180.0f) currentAngle -= 360.0f;
        if (playerMovement.currentBlock.angle != currentAngle)
        {
            AdjustCameraToInclination(currentAngle);
        }
    }

    private void AdjustCameraToInclination(float currentAngle)
    {
        int direction = 1;
        if (playerMovement.currentBlock.angle < currentAngle) direction = -1;
        float nextRotation = currentAngle + (angularSpeed * Time.deltaTime * direction);
        if ((nextRotation - playerMovement.currentBlock.angle) * direction > 0.0f)
        {
            nextRotation = playerMovement.currentBlock.angle;
        }
        Quaternion nextRotationQ = Quaternion.Euler(nextRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = nextRotationQ;
    }
}
