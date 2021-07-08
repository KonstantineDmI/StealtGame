using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    private Movement _movement;
    [SerializeField] private GameObject _player;
    private void Start()
    {
       _movement =  FindObjectOfType<Movement>(); 

    }

    public void Crouch()
    {
        if (!_movement.IsCrouched)
        {
            _movement.IsCrouched = true;
            _movement._animations.SetFloat("Speed", 0);
            _movement._animations.SetBool("IsCrouched", true);
        }
        else
        {
            _movement.IsCrouched = false;
            _movement._animations.SetFloat("Speed", 0);
            _movement._animations.SetBool("IsCrouched", false);
        }
    }

    public void Run()
    {
        _movement.IsRunning = true;
    }

    public void NotRun()
    {
        _movement.IsRunning = false;
    }




    public void UseAlertStaff(GameObject alertStaffGameobject)
    {
        //alertStaffGameobject.GetComponent<TriggerSpot>().isActivated = true;
        alertStaffGameobject.GetComponent<TriggerSpot>().IsTriggered();

    }

    public void UseClimb(Transform climbSpot)
    {
        _movement.Climb(climbSpot);
    }
}
