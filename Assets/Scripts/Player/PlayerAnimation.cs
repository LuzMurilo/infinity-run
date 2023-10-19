using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string RUN_STATE = "RunForwardBattle";
    private const string DIE_STATE = "Die";
    private const string HIT_STATE = "GetHit";
    public string currentState;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;

    private void Start() 
    {
        animator.Play(RUN_STATE);
        currentState = RUN_STATE;
    }

    private void Update() 
    {
        if (playerMovement.isRunning && !playerMovement.isJumping && currentState != RUN_STATE)
        {
            if ( !animator.GetCurrentAnimatorStateInfo(0).IsName(RUN_STATE) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.Play(RUN_STATE);
                currentState = RUN_STATE;
            }
        }
    }

    public void PlayDieAnimation()
    {
        if (currentState == DIE_STATE) return;

        animator.Play(DIE_STATE);
        currentState = DIE_STATE;
    }

    public void PlayHitAnimation()
    {
        animator.Play(HIT_STATE);
        currentState = HIT_STATE;
    }
}
