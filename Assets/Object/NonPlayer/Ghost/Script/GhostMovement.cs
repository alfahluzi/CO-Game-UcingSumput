using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GhostMovement : MonoBehaviour
{
    public Animator animator;
    public Ghost_AnimationEvent animEvent;
    public FielOfView fov;

    public Transform[] defaultRute;
    public Transform target;

    public float ghostDetectionTimeLeft = 4;
    int ruteIndex;
    AIDestinationSetter destinationSetter;
    AIPath aiPath;
    Rigidbody2D rb;

    bool mengejar = false;


    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        ruteIndex = defaultRute.Length;
        destinationSetter.target = defaultRute[ruteIndex % defaultRute.Length];
    }

    void Update()
    {
        if (target != null)
        {
            mengejar = true;
            destinationSetter.target = target;
            ghostDetectionTimeLeft = 4;

        }
        else if (target == null && ghostDetectionTimeLeft < 0 && mengejar)
        {
            mengejar = false;
            var nextRute = ruteIndex % defaultRute.Length;
            destinationSetter.target = defaultRute[nextRute];
        }
        if (aiPath.reachedDestination && destinationSetter.target == target)
        {
            Debug.Log("KESURUPAN!!!");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.GetComponent<PlayerMovement>().IsDie = true;
        }
        else if (aiPath.reachedDestination)
        {
            ruteIndex++;
            var nextRute = ruteIndex % defaultRute.Length;
            destinationSetter.target = defaultRute[nextRute];
        }
        if (ghostDetectionTimeLeft > -1)
        {
            ghostDetectionTimeLeft -= Time.deltaTime;
        }


        animator.SetFloat("X", aiPath.velocity.x);
        animator.SetFloat("Y", aiPath.velocity.y);

        fov.bodyAngle = getBodyAngel(aiPath.velocity.x, aiPath.velocity.y);
    }

    private void FixedUpdate()
    {
        if (fov.visibleTargets.Count > 0)
        {
            target = fov.visibleTargets[0];
        }
        else
        {
            target = null;
        }
    }

    private float getBodyAngel(float x, float y)
    {
        return Mathf.Atan2(x, y) * Mathf.Rad2Deg;
    }
}
