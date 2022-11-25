using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationEvent : MonoBehaviour
{
    SpriteRenderer spriteRendere;
    bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteRendere = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpriteVisible()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }
}
