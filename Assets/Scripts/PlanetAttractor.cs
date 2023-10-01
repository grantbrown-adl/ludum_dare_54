using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAttractor : MonoBehaviour
{
    [SerializeField] private float _gravitationalForce;
    [SerializeField] private float _radius;   
    [SerializeField] private HashSet<Rigidbody2D> _rigidbodies = new();
    [SerializeField] private int _numRigidbodies;
    [SerializeField] private GameObject _squareSprite;
    [SerializeField] private GameObject _cube;

    private void Awake()
    {
        if (_gravitationalForce <= 0) _gravitationalForce = 3.0f;
        if (_radius <= 0) _radius = 5.0f;
        _squareSprite.SetActive(true);
        _cube.SetActive(false);
    }
    void FixedUpdate()
    {
        _squareSprite.SetActive(!GameHandler.Instance.ShowCube);
        _cube.SetActive(GameHandler.Instance.ShowCube);

        _numRigidbodies = _rigidbodies.Count;
        _rigidbodies.Clear();
        foreach (Collider2D objectCollision in Physics2D.OverlapCircleAll(transform.position, _radius))
        {
            Rigidbody2D colliderRigidbody = objectCollision.GetComponent<Rigidbody2D>();
            if (colliderRigidbody != null 
                && !_rigidbodies.Contains(colliderRigidbody) 
                && objectCollision.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                _rigidbodies.Add(objectCollision.GetComponent<Rigidbody2D>());
            }
            if (_rigidbodies == null) return;
        }

        foreach (Rigidbody2D rb2d in _rigidbodies)
        {
            ApplyGravitationalForce(rb2d);
        }
    }

    private void ApplyGravitationalForce(Rigidbody2D rb2d)
    {
        Vector2 direction = transform.position  - rb2d.transform.position;
        float distance = direction.magnitude;
        float forceMagnitude = _gravitationalForce / (distance * distance);

        Vector2 force = direction.normalized * forceMagnitude;

        rb2d.AddForce(force);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
