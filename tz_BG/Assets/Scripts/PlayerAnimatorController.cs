using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController playerController;
    private List<Animator> animators=new List<Animator>();
    private bool isRunning;
    private bool isVictory;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    public void ChangeSingleAnimRunState(Animator animator)
    {
        isRunning = !isRunning;
        animator.SetBool("isRunning", isRunning);
    }
    public void ChangeRunAnimState()
    {
        animators = GetAnimators();
        for (int i = 0; i < animators.Count; i++)
        {
            ChangeSingleAnimRunState(animators[i]);
        }
    }
    public void ChangeVictoryAnimState()
    {
        animators = GetAnimators();
        isVictory = !isVictory;
        for (int i = 0; i < animators.Count; i++)
        {
            animators[i].SetBool("isVictory", isVictory);
        }
    }
    private List<Animator> GetAnimators()
    {
        List<Animator> animators = new List<Animator>();
        for(int i=0;i<playerController.playerParts.Count;i++)
        {
            animators.Add(playerController.playerParts[i].GetComponentInChildren<Animator>());
        }
        return animators;
    }    
}
