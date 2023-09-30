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
    [SerializeField] private float _slideFactor;
    [SerializeField] private float _maximumSpeed;
    [SerializeField] private float _forwardDragAmount;
    [SerializeField] private float _forwardDragTime;
    [SerializeField] private float _rightVelocityRenderLimit;

    [Header("View Variables")]
    [SerializeField] private float _accelerationInput;
    [SerializeField] private float _rotationInput;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private Vector2 _forceVector;
    [SerializeField] private Vector2 _forwardVelocity;
    [SerializeField] private Vector2 _rightVelocity;
    [SerializeField] private float _relativeForwardVelocity;
    [SerializeField] private float _reverseSpeedFactor;

    private float epsilon = 0.01f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        InitialiseVariables();
    }

    private void FixedUpdate()
    {
        ApplyForce();
        HandleRotation();
        ReduceRightVelocity();
    }

    private void ApplyForce()
    {
        _relativeForwardVelocity = Vector2.Dot(transform.up, _rigidbody2D.velocity);

        if ((_relativeForwardVelocity >= _maximumSpeed && _accelerationInput > epsilon) || (_relativeForwardVelocity <= (-_maximumSpeed * _reverseSpeedFactor) && _accelerationInput < -epsilon) || (_rigidbody2D.velocity.sqrMagnitude > _maximumSpeed && _accelerationInput > 0))
        {
            return;
        }

        if (_accelerationInput == 0)
        {
            _rigidbody2D.drag = Mathf.Lerp(_rigidbody2D.drag, _forwardDragAmount, Time.fixedDeltaTime * _forwardDragTime);
        }
        else
        {
            _rigidbody2D.drag = 0;
        }
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

    void ReduceRightVelocity()
    {
        _forwardVelocity = transform.up * Vector2.Dot(_rigidbody2D.velocity, transform.up);
        _rightVelocity = transform.right * Vector2.Dot(_rigidbody2D.velocity, transform.right);

        _rigidbody2D.velocity = _forwardVelocity + _rightVelocity * _slideFactor;
    }

    float GetRightVelocity()
    {
        return Vector2.Dot(transform.right, _rigidbody2D.velocity);
    }

    public bool RenderTrail(out float rightVelocity, out bool braking)
    {
        rightVelocity = GetRightVelocity();
        braking = _accelerationInput < 0 && _relativeForwardVelocity > 0;

        return braking || Mathf.Abs(rightVelocity) > _rightVelocityRenderLimit;
    }

    void InitialiseVariables()
    {
        if(_accelerationSpeed <= 0) _accelerationSpeed = 10.0f;
        if (_rotationSpeed <= 0) _rotationSpeed = 4.0f;
        if (_slideFactor <= 0) _slideFactor = 0.95f;
        if (_maximumSpeed <= 0) _maximumSpeed = 15.0f;
        if (_reverseSpeedFactor < 0) _reverseSpeedFactor = 0.0f;
        if (_forwardDragTime <= 0) _forwardDragTime = 3.0f;
        if (_forwardDragAmount <= 0) _forwardDragAmount = 3.0f;
        if (_rightVelocityRenderLimit < 0) _rightVelocityRenderLimit = 0.0f;
    }
}
