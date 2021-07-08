using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Movement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Player's speed by dif types")]
    [SerializeField]
    private float _walkSpeed = 3f;
    [SerializeField]
    private float _runSpeed = 5f;
    [SerializeField]
    private float _crouchSpeed = 1f;

    private float _gravity = 9.81f;

    [Header("Screen buttons & Joystick")]
    [SerializeField]
    private RectTransform _joyField;
    [SerializeField]
    private Button _crouchButton;
    [SerializeField]
    private Button _sprintButton;
    [SerializeField]
    private Button _useButton;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private CharacterController _characterController;

    private Vector3 _velocity;
    private Vector3 _move;
    private bool _isWalking;
    private bool _isCrouched;

    public Animator _animations;
    public bool _isRunning;

    public float WalkSpeed
    {
        get { return _walkSpeed;}
    }
    public float RunSpeed
    {
        get { return _runSpeed; }
    }
    public float CrouchSpeed
    {
        get { return _crouchSpeed; }
    }

    public bool IsCrouched
    {
        get { return _isCrouched; }
        set { _isCrouched = value; }
    }

    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }


   



    

    

    private void Start()
    {
        _isWalking = false;
        _isRunning = false;
        _isCrouched = false;
        _animations = _player.GetComponent<Animator>();
    }


    private void Update()
    {

        if (_velocity.y < 0) // in case if player in air  -  transform him to the ground with -2 speed.
        {
            _velocity.y = -1f;
        }

        _velocity.y -= _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
        



    }
    


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)_joyField.position, _joyField.rect.width * 0.5f);
        _move = new Vector3(transform.localPosition.x, 0, transform.localPosition.y).normalized;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine("PlayerMove");
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine("PlayerMove");
        _animations.SetFloat("Speed", 0);
        transform.localPosition = Vector3.zero;
        _move = Vector3.zero;
        _isWalking = false;
    }

    IEnumerator PlayerMove()
    {
        _isWalking = true;
        while (true)
        {

            if (!_isCrouched) 
            {
                Walk();
                if (_isRunning)
                {
                    Run();
                }
            }
            else
            {
                CrouchedWalk();
            }

            Rotation();


            yield return null;
        }
        
    }


    private void Walk()
    {
        _animations.SetFloat("Speed", _walkSpeed);
        _characterController.Move(_move * _walkSpeed * Time.deltaTime);
    }

    private void Run()
    {
         _animations.SetFloat("Speed", _runSpeed);
         _characterController.Move(_move * _runSpeed * Time.deltaTime);
    }

    private void Rotation()
    {
        
        if (_move != Vector3.zero)
        {
            _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation, Quaternion.LookRotation(_move), 5 * Time.deltaTime);
        }
            


    }

   

    private void CrouchedWalk()
    {
        _animations.SetFloat("Speed", _crouchSpeed);
        _characterController.Move(_move * _crouchSpeed * Time.deltaTime);
    }


    public void Climb(Transform climbSpot)
    {
        _characterController.enabled = false;
        Transform upperTransform = climbSpot.parent.GetChild(1);
        _characterController.transform.position = new Vector3(upperTransform.position.x, upperTransform.position.y, upperTransform.position.z);
        _characterController.enabled = true;
    }

   

 

}










       