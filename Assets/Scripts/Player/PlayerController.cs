using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _rigidbody2D;

    [Header("Settings")]
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _rotationSpeed;

    [Header("View Variables")]
    [SerializeField] private float _accelerationInput;
    [SerializeField] private float _rotationInput;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private Vector2 _forceVector;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        InitialiseVariables();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyForce();
        HandleRotation();
    }

    private void ApplyForce()
    {
        _forceVector = _accelerationInput * _accelerationSpeed * transform.up;
        _rigidbody2D.AddForce(_forceVector, ForceMode2D.Force);
    }

    private void HandleRotation()
    {
        //minimumTurnSpeed = _rigidbody2D.velocity.magnitude / minimumTurnSpeedFactor;
        //minimumTurnSpeed = Mathf.Clamp(minimumTurnSpeed, 0, 1);


        _rotationAngle -= _rotationInput * _rotationSpeed;
        _rigidbody2D.MoveRotation(_rotationAngle);
    }

    public void HandleInput(Vector2 inputVector)
    {
        _rotationInput = inputVector.x;
        _accelerationInput = inputVector.y;
    }

    void InitialiseVariables()
    {
        _accelerationSpeed = 10.0f;
        _rotationSpeed = 4.0f;
    }
}
