using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaPlayer : MonoBehaviour
{
    public float maxStamina = 120f;
    public float stamina;
    public Slider sldr;
    public bool habis = false;
    public bool onigiriBuff = false;

    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        stamina = maxStamina;
        sldr.maxValue = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && stamina >= 0 && habis == false && playerMovement.IsMove){
            stamina -= 15f * Time.deltaTime;
            
        } else if(stamina <= 0){
            habis = true;
            stamina = 0;
        } else if(stamina >= maxStamina){
            habis = false;
        }

        if(habis || onigiriBuff){
            tambah_stm();
        }

        sldr.value = stamina;
    }

    void tambah_stm()
    {
        stamina += 2f * Time.deltaTime;
    }
}
