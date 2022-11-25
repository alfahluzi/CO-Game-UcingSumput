using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Lantern_Object : MonoBehaviour
{
    public bool isLightOn, readyToActive;
    public Vector3 playerPosition, directPosition;
    public GameObject lightBg, lightMg, lightFg, lightBloom;

    [HideInInspector] public GameObject HintUI, player;
    //public Animator animator;
    public FielOfView lightArea;

    [HideInInspector] public Animator playerAnimator;
    PlayerMovement playerMovement;
    public float angleDirection;
    private string direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.transform.Find("Animation_Glow").GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
        readyToActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        if (readyToActive)
        {
            HintUI.SetActive(true);

            directPosition = playerPosition - gameObject.transform.position;
            angleDirection = Mathf.Atan2(directPosition.x, directPosition.y) * Mathf.Rad2Deg;
            if (angleDirection < 0) angleDirection += 360;

            if (angleDirection > 45 && angleDirection <= 135) direction = "left";
            else if (angleDirection > 135 && angleDirection <= 225) direction = "up";
            else if (angleDirection > 225 && angleDirection <= 315) direction = "right";
            else if (angleDirection > 315 || angleDirection <= 45) direction = "down";
        }
        else if (!readyToActive) HintUI.SetActive(false);

        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_lantern_" + direction))
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 >= 0.9)
            {
                isLightOn = true;
                playerMovement.IsAction = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.E) && readyToActive && !isLightOn)
        {
            playerMovement.IsAction = true;
            playerAnimator.Play("Player_lantern_" + direction);
        }

        else if (Input.GetKeyUp(KeyCode.E) && readyToActive && isLightOn)
        {
            isLightOn = false;
        }

        if (isLightOn)
        {
            lightBg.SetActive(true);
            lightMg.SetActive(true);
            lightFg.SetActive(true);
            lightBloom.SetActive(true);

        }
        else
        {
            lightBg.SetActive(false);
            lightMg.SetActive(false);
            lightFg.SetActive(false);
            lightBloom.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToActive = true;
            playerPosition = new Vector2(collision.transform.position.x, collision.transform.position.y);
            //playerPosition = collision.transform.up;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToActive = false;
        }
    }


}
