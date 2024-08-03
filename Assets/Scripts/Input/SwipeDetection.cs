using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumDistance = .2f;
    [SerializeField] private float maximumTime = 1f;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private void OnEnable() 
    {
        InputManager.Instance.OnTouchStarted.AddListener(SwipeStart);
        InputManager.Instance.OnTouchEnded.AddListener(SwipeEnd);
    }

    private void OnDisable() 
    {
        InputManager.Instance.OnTouchStarted.RemoveListener(SwipeStart);
        InputManager.Instance.OnTouchEnded.RemoveListener(SwipeEnd);
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector2.Distance(startPosition, endPosition) < minimumDistance) return;
        if (endTime - startTime > maximumTime) return;

        Debug.Log("Swipe Detected!");
        Debug.DrawLine(startPosition, endPosition, Color.red, 2f);
    }
}
