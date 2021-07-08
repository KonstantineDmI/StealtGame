using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSpot : MonoBehaviour
{
    private float _botsTriggerRadius = 10f;
    private float _playerTriggerRadius = 5f;


    [SerializeField]
    private LayerMask _botsLayer;

    [SerializeField]
    private LayerMask _playerLayer;

    [SerializeField]
    private Collider[] _botsColliders;

    [SerializeField]
    private Collider[] _playerCollider;


    private bool _isActivated;

    public bool isActivated
    {
        get
        {
            return _isActivated;
        }
        set
        {
            _isActivated = value;
        }
    }


    [SerializeField]
    private Text _useTriggerText;

    [SerializeField]
    private Button _useButton;

    private void Start()
    {
        StartCoroutine("BotsColliders");
        //_isActivated = false;

    }

    private IEnumerator BotsColliders()
    {
        while(true)
        {
            _botsColliders = Physics.OverlapSphere(transform.position, _botsTriggerRadius, _botsLayer);
            yield return new WaitForSeconds(0.5f);
        }
    }

    //private IEnumerator PlayerCollider()
    //{
    //    while (true)
    //    {
    //        _playerCollider = Physics.OverlapSphere(transform.position, _playerTriggerRadius, _playerLayer);
    //        if(_playerCollider.Length > 0)
    //        {
    //            _useTriggerText.text = "Use trigger!";
    //            ButtonIsEnabled(true);
    //        }
    //        else
    //        {
    //            _useTriggerText.text = "";
    //            ButtonIsEnabled(false);
    //        }
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            _useTriggerText.text = "Use trigger!";
            ButtonIsEnabled(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _useTriggerText.text = "";
            ButtonIsEnabled(false);
        }
    }


    public void ButtonIsEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            _useButton.GetComponent<Image>().color = Color.green;
            RemoveListener();
            _useButton.onClick.AddListener(UseButton);

        }
        else
        {
            _useButton.GetComponent<Image>().color = Color.gray;
            RemoveListener();
        }
        
    }

    private void RemoveListener()
    {
        _useButton.onClick.RemoveAllListeners();
    }



    public void IsTriggered() //revork a little bit
    {
        for(int i = 0; i < _botsColliders.Length; i++)
        {
            _botsColliders[i].GetComponent<AI>().Triggered(this.transform);
            this.enabled = false;
        }
    }


    private void UseButton()
    {
        if(this.tag == "Alert")
        {
            _useButton.GetComponent<ButtonsScript>().UseAlertStaff(transform.gameObject);
        }
        else if(this.tag == "ClimbSpot")
        {
            _useButton.GetComponent<ButtonsScript>().UseClimb(transform);
        }
        
    }


    


}
