using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    const string PLAYER_SHOOT_PISTOL = "PistolShoot";

    public bool Dead = false;

    public static AnimationManager Instance;
    private void Awake()
    {
    Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
    }
    void ChangeAnimationState(string newState)
    {
        Animator animator = GetComponent<Animator>();
        if(currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
    // Update is called once per frame
    void Update()
    {
        
        PistolAnimation();
    }
    void PistolAnimation()
    {
        if(!Dead)
        { 
            //ChangeAnimationState(PLAYER_SHOOT_PISTOL);
        }
    }
}
