using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField]
    private GameObject _wayPoints;
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private NavMeshAgent _agent;
    private Vector3 velocity;
    private Vector3 _moveCheck;

    [SerializeField]
    private float _walkSpeed = 1;
    [SerializeField]
    private float _runSpeed = 3;
    private int _currentPointIndex = 0;

    [SerializeField]
    private Animator _animations;

    [SerializeField]
    private bool _isWalking;
    [SerializeField]
    private bool _isRunning;
    private bool _isMoving;

    public bool _triggeredOnSpot;
    public bool _triggeredOnPlayer;


    private void Start()
    {
        _isWalking = false;
        _isRunning = false;
        _triggeredOnSpot = false;
        _triggeredOnPlayer = false;

        StartCoroutine("IsMoving");
        StartCoroutine("MoveAnimations");


    }





    private void Update()
    {

        WaypointsWalk();
        
        
    }

    private void WaypointsWalk()      // mabye it should be like coroutine
    {
        if (!IsTriggered() && _currentPointIndex < _wayPoints.transform.childCount)
        {
            _agent.speed = _walkSpeed;
            _agent.stoppingDistance = 0;
            GameObject currentPoint = _wayPoints.transform.GetChild(_currentPointIndex).gameObject;
            
            if(Vector3.Distance(transform.position, currentPoint.transform.position) > 1)
            {
                //_isWalking = true;
                _agent.SetDestination(new Vector3(currentPoint.transform.position.x, transform.position.y, currentPoint.transform.position.z));
                transform.LookAt(Vector3.SmoothDamp(transform.position, currentPoint.transform.position, ref velocity, 10f));
            }
            else
            {
                _currentPointIndex++;
            }

            
        }
        else
        {
            _currentPointIndex = 0;
        }
       
      
    }

    

    public void Triggered(Transform targetTransform)
    {
        //_triggered = true;
        //_agent.stoppingDistance = 2;
        //_agent.speed = _runSpeed;
        //AgentDestination(targetTransform);
        Debug.Log(targetTransform.name);
        if(targetTransform.name == "Ken")
        {
            StartCoroutine("Kill", targetTransform);
        }
        else
        {
            StartCoroutine("Test", targetTransform);
        }
        

    }

    

    

    private IEnumerator MoveAnimations()
    {
        while (true)
        {
            if (_isMoving)
            {
                if (!IsTriggered())
                {
                    _isWalking = true;
                    _animations.SetFloat("Speed", _walkSpeed);
                }
                else
                {
                    _isRunning = true;
                    _animations.SetFloat("Speed", _runSpeed);
                }
            }
            else
            {
                _animations.SetFloat("Speed", 0);
            }
            

            yield return null;
        }

        
    }

    private IEnumerator IsMoving()
    {
        while (true)
        {
            _moveCheck = transform.position;
            yield return new WaitForSeconds(0.1f);
            if(_moveCheck != transform.position)
            {
                _moveCheck = transform.position;
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }
        }
    }

    private void AgentDestination(Transform targetTransform)
    {
        _agent.SetDestination(targetTransform.position);
        transform.LookAt(targetTransform.position);
    }

    private IEnumerator Test(Transform target)
    {
        _triggeredOnSpot = true;
        while (true)
        {
            if (!_triggeredOnPlayer)
            {
                if (Vector3.Distance(_agent.transform.position, target.position) > 3)
                {
                    _agent.stoppingDistance = 1;
                    _agent.speed = _runSpeed;
                    AgentDestination(target);
                    yield return null;
                }
                else
                {
                    _agent.speed = 0;
                    yield return new WaitForSeconds(3f);
                    _triggeredOnSpot = false;
                    StopCoroutine("Test");
                }
            }
            else
            {
                StopCoroutine("Test");
            }
            
        }
    }

    private IEnumerable Kill(Transform target)
    {
        _triggeredOnSpot = false;
        _triggeredOnPlayer = true;
        while (true)
        {
            if (Vector3.Distance(_agent.transform.position, target.position) > 1)
            {
                _agent.stoppingDistance = 1;
                _agent.speed = _runSpeed;
                AgentDestination(target);
                yield return null;
            }
        }
    }


    private bool IsTriggered()
    {
        if(_triggeredOnPlayer || _triggeredOnSpot)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
