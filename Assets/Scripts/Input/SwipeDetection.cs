using UnityEngine;
using UnityEngine.Events;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumDistance = .2f;
    [SerializeField] private float maximumTime = 1f;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    public UnityEvent OnSwipeUp;
    public UnityEvent OnSwipeDown;
    public UnityEvent OnSwipeLeft;
    public UnityEvent OnSwipeRight;

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
        //Debug.Log("distance = " + Vector2.Distance(startPosition, endPosition));
        //Debug.Log("time = " + (endTime - startTime));
        if (Vector2.Distance(startPosition, endPosition) < minimumDistance) return;
        if (endTime - startTime > maximumTime) return;

        Vector2 direction = (endPosition - startPosition).normalized;
        if (Vector2.Dot(direction, Vector2.up) > 0.5f)
        {
            OnSwipeUp.Invoke();
        }
        else if (Vector2.Dot(direction, Vector2.down) > 0.5f)
        {
            OnSwipeDown.Invoke();
        }
        else if (Vector2.Dot(direction, Vector2.right) > 0.5f)
        {
            OnSwipeRight.Invoke();
        }
        else if (Vector2.Dot(direction, Vector2.left) > 0.5f)
        {
            OnSwipeLeft.Invoke();
        }
    }

}
