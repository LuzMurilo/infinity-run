using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumDistance = .2f;
    [SerializeField] private float maximumTime = 1f;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    private bool swipeStarted;
    private InputAction touchAction;

    public UnityEvent OnSwipeUp;
    public UnityEvent OnSwipeDown;
    public UnityEvent OnSwipeLeft;
    public UnityEvent OnSwipeRight;

    private void OnEnable() 
    {
        InputManager.Instance.OnTouchStarted.AddListener(SwipeStart);
        InputManager.Instance.OnTouchEnded.AddListener(SwipeEnd);
        swipeStarted = false;
    }

    private void OnDisable() 
    {
        InputManager.Instance.OnTouchStarted.RemoveListener(SwipeStart);
        InputManager.Instance.OnTouchEnded.RemoveListener(SwipeEnd);
        swipeStarted = false;
    }

    private void Update() 
    {
        if (swipeStarted)
        {
            DetectSwipe(touchAction.ReadValue<Vector2>());
        }
    }

    private void SwipeStart(InputAction action, float time)
    {
        touchAction = action;
        Vector2 position = action.ReadValue<Vector2>();
        startPosition = position;
        startTime = time;
        swipeStarted = true;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        touchAction = null;
        endPosition = position;
        endTime = time;
        //DetectSwipe();
        swipeStarted = false;
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

    private void DetectSwipe(Vector2 newPosition)
    {
        if (Vector2.Distance(startPosition, newPosition) < minimumDistance) return;
        swipeStarted = false;
        Vector2 direction = (newPosition - startPosition).normalized;
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
