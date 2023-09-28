using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _checkGroundRadious;
    [SerializeField] private float _cameraWalkShakeSpeed;
    [SerializeField] private float _cameraRunShakeSpeed;

    [SerializeField] private Transform _groundCheckPivot;
    
    [SerializeField] private LayerMask _groundMask;

    private float _velocity;
    private bool _isGrounded;

    private CharacterController _characterController;
    
    private Vector3 _moveDirection;

    private bool _isRunning;
    
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _isGrounded = IsGrounded();
        if (_isGrounded && _velocity < 0) 
            _velocity = -2;
        
        Move();
        Run();
        Gravity();
    }

    private void Update()
    {
        Jump();
    }

    private void Move()
    {
        _moveDirection = new Vector3(MoveX(), 0, MoveZ());
        _moveDirection *= _isRunning ? _runSpeed : _walkSpeed;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _isRunning = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            _isRunning = false;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _velocity = Mathf.Sqrt(_jumpForce * -2 * _gravity);
    }

    private void Gravity()
    {
        _velocity += _gravity * Time.fixedDeltaTime;
        _characterController.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
    }

    private bool IsGrounded()
    {
        bool result = Physics.CheckSphere(_groundCheckPivot.position, _checkGroundRadious, _groundMask);
        return result;
    }

    private float MoveX()
    {
        return Input.GetAxis("Horizontal");
    }

    private float MoveZ()
    {
        return Input.GetAxis("Vertical");
    }

}
