using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private string RUN_STATE = "RunForwardBattle";
    private string DIE_STATE = "Die";
    [SerializeField] private Animator animator;

    private void Start() 
    {
        animator.Play(RUN_STATE);
    }

    public void PlayDieAnimation()
    {
        animator.Play(DIE_STATE);
    }
}
