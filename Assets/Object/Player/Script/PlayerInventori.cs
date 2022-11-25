using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventori : MonoBehaviour
{


    private string lup = "LUP";
    private string onigiri = "ONIGIRI";
    private string botol = "BOTOL";

    public GameObject[] slotItem = new GameObject[3];
    [HideInInspector] public string[] items = new string[3];
    [HideInInspector] public bool[] isFull = new bool[3];
    
    Player_BuffEffect buffEffect;

    private void Start()
    {
        buffEffect = GetComponent<Player_BuffEffect>();
    }
    private void Update()
    {
        BuffReadyDetect();
        if (isFull[0] && Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (items[0] == lup)
                lupEffect(0);
            else if (items[0] == onigiri)
                onigiriEffect(0);
            else if (items[0] == botol)
                botolEffect(0);
        }

        if (isFull[1] && Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (items[1] == lup)
                lupEffect(1);
            else if (items[1] == onigiri)
                onigiriEffect(1);
            else if (items[1] == botol)
                botolEffect(1);
        }

        if (isFull[2] && Input.GetKeyUp(KeyCode.Alpha3))
        {
            if (items[2] == lup)
                lupEffect(2);
            else if (items[2] == onigiri)
                onigiriEffect(2);
            else if (items[2] == botol)
                botolEffect(2);
        }

    }

    private void BuffReadyDetect()
    {
        if (items[0] == lup && isFull[0] || items[1] == lup && isFull[1] || items[2] == lup && isFull[2])
            buffEffect.lupIsReady = true;
        else
            buffEffect.lupIsReady = false;
        if (items[0] == botol && isFull[0] || items[1] == botol && isFull[1] || items[2] == botol && isFull[2])
            buffEffect.botolIsReady = true;
        else
            buffEffect.botolIsReady = false;
        if (items[0] == onigiri && isFull[0] || items[1] == onigiri && isFull[1] || items[2] == onigiri && isFull[2])
            buffEffect.oniIsReady = true;
        else
            buffEffect.oniIsReady = false;
    }

    void lupEffect(int i)
    {
        buffEffect.lupIsActive = true;
        isFull[i] = false;
        Destroy(slotItem[i].transform.GetChild(0).gameObject);
    }

    void botolEffect(int i)
    {
        buffEffect.botolIsActive = true;
        isFull[i] = false;
        Destroy(slotItem[i].transform.GetChild(0).gameObject);
    }

    void onigiriEffect(int i)
    {
        buffEffect.oniIsActive = true;
        isFull[i] = false;
        Destroy(slotItem[i].transform.GetChild(0).gameObject);
    }
}
