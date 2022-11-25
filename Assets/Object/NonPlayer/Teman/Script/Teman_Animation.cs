using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Teman_Animation : MonoBehaviour
{
    public Animator animator;
    public AIPath aiPath;

    private string lastMove = "down";
    private AnimatorClipInfo[] _currentClipInfo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("X", aiPath.velocity.x);
        animator.SetFloat("Y", aiPath.velocity.y);
        if (aiPath.velocity.x > 0.3 || aiPath.velocity.x < -0.3 || aiPath.velocity.y > 0.3 || aiPath.velocity.y < -0.3)
        {
            lastMove = GetLastMove();
            Debug.Log(lastMove);
        }
        if (aiPath.velocity.x < 0.1 && aiPath.velocity.x > -0.1 && aiPath.velocity.y < 0.1 && aiPath.velocity.y > -0.1)
        {
            animator.Play("Teman_idle_" + lastMove);
        }
        else
        {
            animator.Play("Walking");
        }
    }

    string GetLastMove()
    {
        _currentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
        string temp = _currentClipInfo[0].clip.name;
        char[] charTemp = temp.ToCharArray();
        List<char> charList = new List<char>();
        int cond = 0;
        foreach (char ch in charTemp)
        {
            if (cond == 2)
                charList.Add(ch);
            if (ch == '_')
                cond++;
        }
        charTemp = new char[charList.Count];
        charTemp = charList.ToArray();
        string retr = new string(charTemp);
        return retr;
    }
}
