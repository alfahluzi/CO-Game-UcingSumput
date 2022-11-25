using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimationClipDictionary : MonoBehaviour
{
    [Serializable]
    public struct AniClip
    {
        public string tittle;
        public AnimationClip clip;
    }

    public AniClip[] aniClipList;
    Dictionary<string, AnimationClip> aniClipDiction = new Dictionary<string, AnimationClip>();

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < aniClipList.Length; i++)
        {
            aniClipDiction.Add(aniClipList[i].tittle, aniClipList[i].clip);
        }

    }

}
