using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _movement;
    private string _lastPosition = "down";
    [SerializeField] private bool _isMove, _isDie, _isAction, _isHiding;

    public Animator animator;
    public StaminaPlayer stamina;
    public Rigidbody2D rigidbody;
    public GameObject onHideLight, PlayerLightingParent;
    public SpriteRenderer spriteRenderer;
    public GameObject lightBg, lightMg, lightFg, lightBloom;
    public Material normalMaterial, bloomMaterial;

    public float speedPoint = 1;
    [HideInInspector] public float speed;
    public bool IsMove
    {
        get { return _isMove; }
        set { _isMove = value; }
    }
    public bool IsDie
    {
        get { return _isDie; }
        set { _isDie = value; }

    }
    public bool IsAction
    {
        get { return _isAction; }
        set { _isAction = value; }
    }
    public bool IsHiding
    {
        get { return _isHiding; }
        set { _isHiding = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        IsAction = false;
        IsHiding = false;

        IsDie = false;
        stamina = GetComponent<StaminaPlayer>();
        speed = speedPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHiding) HidingState();
        else NotHidingState();

        if (!IsAction && !IsDie) InputProcessing();

        if (IsDie)
        {
            lightBg.SetActive(false);
            lightMg.SetActive(false);
            lightFg.SetActive(false);
            lightBloom.SetActive(true);
            speed = 0;
            spriteRenderer.material = normalMaterial;
            string animationName = "die_" + _lastPosition;
            animator.Play(animationName);
            //StartCoroutine(willPausedIn(0.5f));
        }
        else spriteRenderer.material = bloomMaterial;
    }
    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + _movement * speed * Time.fixedDeltaTime);
    }


    IEnumerator willPausedIn(float timeInSecond)
    {
        yield return new WaitForSecondsRealtime(timeInSecond);
        Time.timeScale = 0;
    }
    private float getBodyAngel(float x, float y)
    {
        return Mathf.Atan2(x, y) * Mathf.Rad2Deg;
    }

    private void HidingState()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0);
        PlayerLightingParent.SetActive(false);
        onHideLight.SetActive(true);
    }
    private void NotHidingState()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        PlayerLightingParent.SetActive(true);
        onHideLight.SetActive(false);
    }
    private void InputProcessing()
    {
        if (IsHiding)
            return;

        animator.speed = 1 + speed / 10;
        if (Input.GetKeyDown(KeyCode.LeftShift) && !stamina.habis && IsMove)
        {
            speed = 2 * speedPoint;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || stamina.habis == true)
        {
            speed = speedPoint;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("walking", true);
            animator.Play("Player_move");
            IsMove = true;
        }
        else
        {
            IsMove = false;
            animator.SetBool("walking", false);
        }

        if (Input.GetKeyUp(KeyCode.W))
            _lastPosition = "up";
        else if (Input.GetKeyUp(KeyCode.A))
            _lastPosition = "left";
        else if (Input.GetKeyUp(KeyCode.S))
            _lastPosition = "down";
        else if (Input.GetKeyUp(KeyCode.D))
            _lastPosition = "right";

        if (!IsMove && !IsDie)
        {
            animator.Play("Player_idle_" + _lastPosition);
        }

        _movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("move_x", _movement.x);
        _movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("move_y", _movement.y);


    }
    public void LogTest(string log)
    {
        Debug.Log(log);
    }
}
