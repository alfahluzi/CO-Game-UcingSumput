using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : MonoBehaviour
{
    public GameObject thisObject, hidingSpriteObj, notHidingSpriteObj, hintUI;

    [HideInInspector] public string hideDirection;
    public Transform lastPos;
    public bool isHiding, inHidingArea;

    bool InputAccess = true; // false only if hide animation is still playing;

    private PlayerMovement _playerMovement;
    private GameObject _playerObj;
    private Animator _playerAnimator;
    private AnimationClip[] _animationClips;
    private FielOfView _playerFov;
    private Collider2D _playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        inHidingArea = false;
        isHiding = false;
        setActiveObject(isHiding);
        hintUI.SetActive(false);
        if (_playerObj == null) _playerObj = GameObject.Find("Player");

        if (_playerObj != null) Debug.Log("playerObj berhasil ditemukan");
        else Debug.Log("playerObj tidak ditemukan");

        _playerMovement = _playerObj.GetComponent<PlayerMovement>();
        _playerMovement.LogTest("Berhasil akses script PlayerMovement");

        _playerAnimator = _playerObj.GetComponentInChildren<Animator>();
        if (_playerAnimator != null) Debug.Log("_playerAnimator berhasil ditemukan");
        else Debug.Log("_playerAnimator tidak ditemukan");

        _playerCollider = _playerObj.GetComponent<CapsuleCollider2D>();
        if (_playerCollider != null) Debug.Log("_playerCollider berhasil ditemukan");
        else Debug.Log("_playerCollider tidak ditemukan");
    }

    // Update is called once per frame
    void Update()
    {
        if (inHidingArea)
            hintUI.SetActive(true);

        if (InputAccess)
            ButtonAction();

        setActiveObject(isHiding);

    }

    private void ButtonAction()
    {
        //Diluar otw masuk
        if (Input.GetKeyDown(KeyCode.E) && isHiding == false && inHidingArea)
        {
            StartCoroutine(GettingIn());

        }

        //Didalam otw keluar
        else if (Input.GetKeyDown(KeyCode.E) && isHiding == true && inHidingArea)
        {

            StartCoroutine(GettingOut());

        }
    }
    IEnumerator GettingIn()
    {
        InputAccess = false;
        _playerMovement.IsAction = true;

        _playerCollider.isTrigger = true;
        //Check Direction->get last position->set animation name->play hide animation->change player position
        hideDirection = getDirection(transform.position, _playerObj.transform.position);
        lastPos.position = _playerObj.transform.position;
        string animationName = "Player_Hiding_" + hideDirection;
        float waitTime = getAnimationClipTime(animationName);

        _playerAnimator.Play(animationName);
        yield return new WaitForSeconds(waitTime);

        //isHiding change to true, playerCollider trigger change to false
        isHiding = true;
        _playerMovement.IsHiding = true;
        Debug.Log("Udah Masuk");
        _playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        _playerObj.transform.position = transform.position;
        InputAccess = true;
        _playerMovement.IsAction = false;
    }

    IEnumerator GettingOut()
    {
        InputAccess = false;
        _playerMovement.IsAction = true;
        _playerCollider.isTrigger = false;
        _playerMovement.IsHiding = false;
        //set animation name -> play hide animation -> change player position
        string animationName = "Player_Hiding_" + hideDirection;
        float waitTime = getAnimationClipTime(animationName);

        _playerObj.transform.position = lastPos.position;
        _playerAnimator.Play(animationName + "_Reverse");
        yield return new WaitForSeconds(waitTime);

        //isHiding change to false, playerCollider trigger change to true
        isHiding = false;

        Debug.Log("Udah Keluar");
        _playerObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        InputAccess = true;
        _playerMovement.IsAction = false;
    }


    private float getAnimationClipTime(string clipName)
    {

        _animationClips = _playerAnimator.runtimeAnimatorController.animationClips;
        foreach (var clip in _animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        return -1;
    }
    private string getDirection(Vector3 thisObjPos, Vector3 targetPos)
    {
        Vector2 targetDir = (targetPos - thisObjPos);
        var angle = Vector2.Angle(targetDir, transform.up);
        if (targetDir.x < 0) angle *= -1;

        Debug.Log(angle);
        if (angle >= -60 && angle < 60)//target diatas obj
        {
            return "down";
        }
        else if (angle >= -120 && angle < -60)//target dikiri obj
        {
            return "right";
        }
        else if (angle >= 60 && angle < 120)//target dikanan obj
        {
            return "left";
        }
        else  //target dibawah obj
        {
            return "up";
        }
    }
    void setActiveObject(bool _hiding)
    {
        hidingSpriteObj.SetActive(_hiding);
        notHidingSpriteObj.SetActive(!_hiding);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inHidingArea = true;
            hintUI.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inHidingArea = false;
            hintUI.SetActive(false);
        }
    }
}
