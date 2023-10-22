using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

public abstract class Monster : MonoBehaviour
{
    [Header("Stats")] 
    [SerializeField] protected float _speed;

    [Header("Patrol Movement")] 
    [SerializeField] protected bool _isPatrol;
    [SerializeField] private Transform[] _movePoints;
    
    [Header("Wandering Movement")]
    [SerializeField] protected bool _isWander;

    private Animator _animator;
    
    private Vector3 _lastPlayerPosition;

    private Rigidbody2D _rigidbody2D;
    
    private int _currentPointIndex = 0;
    private Transform _currentPoint;
    
    private Vector3 _initialPosition;
    private Vector3 _wanderTargetPosition;
    
    private bool _isFollowingPlayer;

    protected virtual void Awake()
    {
        CheckIfOneMovementOn();
    }
    protected virtual void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _initialPosition = transform.position;
        _animator.SetInteger("moving", 1);
    }
    protected virtual void Update()
    {
        if (_lastPlayerPosition != Vector3.zero)
        {
            MoveToPlayer();
        }
        else
        {
            if (_isPatrol)
                PatrolMovement();
            if (_isWander)
                WanderingMovement();
        }
    }

    protected void OnTriggerStay(Collider coll)
    {
        DetectPlayer(coll);
    }

    private void OnTriggerExit(Collider coll)
    {
        _isFollowingPlayer = false;
    }

    private void PatrolMovement()
    {
        if (_currentPoint == null)
        {
            _currentPoint = _movePoints[_currentPointIndex];
        }

        // Направление к текущей цели
        Vector3 directionToTarget = (_currentPoint.position - transform.position).normalized;
        if (directionToTarget != Vector3.zero)
        {
            // Поворот монстра к цели
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Перемещение к текущей цели
        transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _currentPoint.position) < 1f)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _movePoints.Length;
            _currentPoint = _movePoints[_currentPointIndex];
        }
    }

    private void WanderingMovement()
    {
        if (_isWander)
        {
            // Если не выбрана цель блуждания, выбираем новую
            if (!_isWander)
            {
                _wanderTargetPosition = GenerateRandomTargetPosition();
                _isWander = true;
            }

            // Поворот монстра в сторону цели
            Vector3 directionToTarget = (_wanderTargetPosition - transform.position).normalized;
            if (directionToTarget != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
            }

            // Перемещение монстра
            transform.position = Vector3.MoveTowards(transform.position, _wanderTargetPosition, _speed * Time.deltaTime);

            // Проверка достижения цели и возврат на начальную позицию
            if (Vector3.Distance(transform.position, _wanderTargetPosition) < 1f)
            {
                // Возврат к начальной позиции
                transform.position = _initialPosition;
            }
        }
    }
    private Vector3 GenerateRandomTargetPosition()
    {
        Vector3 randomDirection = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));
        return _initialPosition + randomDirection * 10f;
    }

    private void DetectPlayer(Collider coll)
    {
        if (coll.gameObject.TryGetComponent<FirstPersonController>(out FirstPersonController playerController))
        {
            _lastPlayerPosition = coll.gameObject.transform.position;
            _isFollowingPlayer = true;
        }
    }

    private void DeleteLastPos()
    {
        if (_lastPlayerPosition != Vector3.zero && Vector3.Distance(transform.position, _lastPlayerPosition) < 1f)
        {
            _lastPlayerPosition = Vector3.zero;
            _isFollowingPlayer = false;
        }
    }

    private void MoveToPlayer()
    {
        Vector3 directionToPlayer = (_lastPlayerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        transform.position = Vector3.MoveTowards(transform.position, _lastPlayerPosition, _speed * Time.deltaTime);
        DeleteLastPos();
    }

    private void CheckIfOneMovementOn()
    {
        if (_isPatrol && _isWander)
            _isPatrol = false;
    }
}
