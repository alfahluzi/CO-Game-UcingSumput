using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_AnimationEvent : MonoBehaviour
{
    private Animator animator;

    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isJumping = false;
    }
    // Update is called once per frame

    public void StartJumping()
    {
        isJumping = true;
    }

    public void StopJumping()
    {
        isJumping = false;
    }
    
}
